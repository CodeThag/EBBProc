using DAL;
using EBBProc.Areas.Me.Models;
using EBBProc.Common;
using EBBProc.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace EBBProc.Areas.Me.Controllers
{
    [Authorize]
    public class OrganisationController : Controller
    {
        EBBPEntities db = new EBBPEntities();

        // GET: Me/Organisation
        public ActionResult Index()
        {
            OrganisationViewModel ViewModel = new OrganisationViewModel();
            ViewModel.Organisations = db.Organisations.Where(x => x.AspNetUsers.Any(y => y.UserName == User.Identity.Name)).ToList();
            ViewModel.Registrars = new SelectList(db.CorporateRegistrars, "Id", "Name");
            ViewModel.States = new SelectList(db.States, "Id", "Name");
            ViewModel.LGAs = new SelectList(db.LGAs.Take(30), "Id", "Name");
            return View(ViewModel);
        }

        // GET: Me/Organisation/Details/5
        public ActionResult Details(Guid id)
        {
            try
            {
                Organisation o = db.Organisations.Find(id);
                if (null == o)
                {
                    ControllerMessage.Error(this, "Invalid organisation request!");
                    return RedirectToAction("index");
                }

                OrganisationViewModel ViewModel = new OrganisationViewModel
                {
                    Row = o,
                    OrganisationDocumentForm = new OrganisationDocumentForm
                    {
                        OrganisationId = o.Id
                    }
                };

                ViewModel.OrganisationForm = new OrganisationForm
                {
                    Id = o.Id,
                    RegistrarId = o.RegistrarId,
                    RCNumber = o.RCNumber,
                    TIN = o.TIN,
                    Website = o.Website,
                    RegistrationYear = o.RegistrationYear,

                };

                ViewModel.DocumentTypes = new SelectList(db.DocumentTypes
                    .Where(x => x.DocumentCategory.Name == "Vendor Registration").ToList(), "Id", "Name");

                return View(ViewModel);
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                ControllerMessage.Error(this, "System Error");
                return RedirectToAction("index");
            }
        }

        public ActionResult AddDocument(OrganisationViewModel ViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var form = ViewModel.OrganisationDocumentForm;
                    Organisation o = db.Organisations.Find(form.OrganisationId);
                    if (null == o)
                    {
                        ControllerMessage.Error(this, "Invalid form!");
                        return RedirectToAction("index");
                    }

                    DocumentType type = db.DocumentTypes.Find(form.DocumentType);
                    if (null == type)
                    {
                        ControllerMessage.Error(this, "Invalid form!");
                        return RedirectToAction("index");
                    }

                    string fileExtension = Path.GetExtension(form.File.FileName);
                    string fileName = Utility.GenerateRandomString(DateTime.Now.Ticks.ToString()) + fileExtension;
                    // Check the extension to ensure compartibility 
                    if (!type.DocumentFormat.Name.Equals(fileExtension.ToLower()))
                    {
                        ControllerMessage.Error(this, "Invalid file type! Please upload a document with " + type.DocumentFormat.Name + " format");
                        return RedirectToAction("details", new { Id = form.OrganisationId });
                    }

                    if (type.UploadSize < form.File.ContentLength)
                    {
                        ControllerMessage.Error(this, "The file should be less than " + type.UploadSize + "kb");
                        return RedirectToAction("details", new { Id = form.OrganisationId });
                    }

                    // Save the file to a location
                    var uploadPath = Server.MapPath(SystemSettings.GetStringValue("DocumentPath"));
                    form.File.SaveAs(uploadPath + fileName);

                    Document document = new Document
                    {
                        Id = Guid.NewGuid(),
                        TypeId = type.Id,
                        Name = fileName,
                        Path = fileName,
                        Size = form.File.ContentLength,
                        CreatedBy = User.Identity.Name,
                        CreatedDate = DateTime.Now
                    };

                    db.Documents.Add(document);
                    o.Documents.Add(document);

                    db.SaveChanges();
                    ControllerMessage.Success(this, "Document uploaded!");
                    return RedirectToAction("details", new { Id = o.Id });
                }

                ControllerMessage.Error(this, "Fill form properly!");
                return RedirectToAction("details", new { Id = ViewModel.OrganisationDocumentForm.OrganisationId });
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                ControllerMessage.Error(this, "System Error");
                return RedirectToAction("index");
            }
        }

        public FileResult Download(Guid Id)
        {
            Document document = db.Documents.Find(Id);
            if (null == document)
            {
                throw new HttpException(404, "File not found");
            }
            var uploadPath = Server.MapPath(SystemSettings.GetStringValue("DocumentPath"));
            var fileName = uploadPath + document.Name;
            return File(fileName, document.DocumentType.DocumentFormat.ContentType, fileName);
        }

        public ActionResult RemoveDocument(Guid Id, Guid OrganisationId)
        {
            Document document = db.Documents.Find(Id);
            if (null == document)
            {
                ControllerMessage.Error(this, "Document not found");
                return RedirectToAction("details", new { Id = OrganisationId });
            }

            var uploadPath = Server.MapPath(SystemSettings.GetStringValue("DocumentPath"));
            var fileName = uploadPath + document.Name;
            var exist = System.IO.File.Exists(fileName);
            if (exist)
            {
                Organisation o = db.Organisations.Find(OrganisationId);
                if (null == o)
                {
                    ControllerMessage.Error(this, "Invalid request");
                    return RedirectToAction("index");
                }

                System.IO.File.Delete(fileName);
                o.Documents.Remove(document);
                db.Documents.Remove(document);

                db.SaveChanges();
                ControllerMessage.Success(this, "Document deleted!");
            }

            return RedirectToAction("details", new { id = OrganisationId });

        }

        public JsonResult GetLocalGovernmentAreas(int stateId)
        {
            StringBuilder str = new StringBuilder();
            foreach (var l in db.LGAs.Where(x => x.StateId == stateId))
            {
                str.Append("<option value='" + l.Id + "'>" + l.Name.ToLower() + "</option>");
            }
            JsonResponse json = new JsonResponse();
            json.Status = true;
            json.Payload = str.ToString();

            return Json(json, JsonRequestBehavior.AllowGet);
        }

        // GET: Me/Organisation/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Me/Organisation/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OrganisationViewModel ViewModel)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    var form = ViewModel.OrganisationForm;
                    Organisation organisation = new Organisation
                    {
                        Id = Guid.NewGuid(),
                        Name = form.Name,
                        RegistrarId = form.RegistrarId,
                        RCNumber = form.RCNumber,
                        TIN = form.TIN,
                        Website = form.Website,
                        RegistrationYear = form.RegistrationYear,
                        Status = Guid.Parse("2B2E6D31-D826-4153-ABBD-16E366D4EEFC"),
                        CreatedBy = User.Identity.Name,
                        CreatedDate = DateTime.Now
                    };

                    db.Organisations.Add(organisation);

                    var user = db.AspNetUsers.Where(x => x.UserName == User.Identity.Name).FirstOrDefault();
                    user.Organisations.Add(organisation);

                    var addressForm = form.AddressForm;
                    var addressType = db.AddressTypes.FirstOrDefault();
                    Address address = new Address
                    {
                        Id = Guid.NewGuid(),
                        Street = addressForm.Street,
                        LGAId = addressForm.LGAId,
                        PhoneNumber = addressForm.PhoneNumber,
                        AltPhoneNumber = addressForm.AltPhoneNumber,
                        PostalCode = addressForm.PostalCode,
                        Email = addressForm.Email,
                        AddressTypeId = addressType.Id,
                        CreatedBy = User.Identity.Name,
                        CreatedDate = DateTime.Now
                    };

                    address.Organisations.Add(organisation);
                    db.Addresses.Add(address);
                    db.SaveChanges();

                    ControllerMessage.Success(this, "Company detail saved!");
                    return RedirectToAction("details", new { Id = organisation.Id });
                }

                ControllerMessage.Error(this, "Invalid form submission!");
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                ControllerMessage.Error(this, "System error! Contact us if this is persistance");
                return RedirectToAction("Index");
            }
        }

        // POST: Me/Organisation/Edit/5
        [HttpPost]
        public ActionResult Edit(OrganisationViewModel ViewModel)
        {
            try
            {
                // TODO: Add update logic here
                var form = ViewModel.OrganisationForm;
                if (ModelState.IsValid)
                {
                    Organisation o = db.Organisations.Find(form.Id);
                    if (null == o)
                    {
                        ControllerMessage.Error(this, "Organisation not found!");
                        return RedirectToAction("details", new { Id = form.Id });
                    }

                    o.Name = form.Name;
                    o.RCNumber = form.RCNumber;
                    o.RegistrarId = form.RegistrarId;
                    o.TIN = form.TIN;
                    o.Website = form.Website;
                    o.RegistrationYear = form.RegistrationYear;
                    o.ModifiedBy = User.Identity.Name;
                    o.ModifiedDate = DateTime.Now;

                    db.SaveChanges();
                    ControllerMessage.Success(this, "Company details updated!");
                    return RedirectToAction("details", new { Id = form.Id });
                }

                ControllerMessage.Error(this, "Invalid form!");
                return RedirectToAction("details", new { Id = form.Id });
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                ControllerMessage.Error(this, "System error! Contact us if this is persistance");
                return RedirectToAction("Index");
            }
        }

        // GET: Me/Organisation/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Me/Organisation/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
