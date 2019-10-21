using DAL;
using EBBProc.Areas.Admin.Models;
using EBBProc.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EBBProc.Areas.Admin.Controllers
{
    [Authorize]
    public class ManageOrganisationController : Controller
    {
        EBBPEntities db = new EBBPEntities();


        // GET: Admin/ManageOrganisation
        public ActionResult Index()
        {
            try
            {
                MOrganisationViewModel ViewModel = new MOrganisationViewModel();
                ViewModel.Organisations = db.Organisations.ToList();
                return View(ViewModel);
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                ControllerMessage.Error(this, "System error! Contact us if this is persistance");
                return RedirectToAction("Index");
            }
        }

        // GET: Admin/ManageOrganisation/Details/5
        public ActionResult Details(Guid id)
        {
            try
            {
                var organisation = db.Organisations.Find(id);
                if (null == organisation)
                {
                    ControllerMessage.Error(this, "Invalid organisation!");
                    return RedirectToAction("index");
                }

                MOrganisationViewModel ViewModel = new MOrganisationViewModel();
                ViewModel.Row = organisation;
                ViewModel.StatusForm = new StatusForm
                {
                    Id = id,
                    Status = organisation.Status
                };
                ViewModel.OrganisationStatus = new SelectList(db.OrganisationStatus, "Id", "Name");
                return View(ViewModel);
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                ControllerMessage.Error(this, "System error! Contact us if this is persistance");
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Status(MOrganisationViewModel ViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var form = ViewModel.StatusForm;

                    var organisation = db.Organisations.Find(form.Id);
                    if (null == organisation)
                    {
                        ControllerMessage.Error(this, "Invalid organisation!");
                        return RedirectToAction("index");
                    }

                    if (organisation.OrganisationStatu.Id.Equals(form.Status))
                    {
                        ControllerMessage.Error(this, "Select a different status");
                        return RedirectToAction("details", new { id = form.Id });
                    }

                    organisation.Status = form.Status;
                    db.SaveChanges();

                    ControllerMessage.Success(this, "Company status: " + organisation.OrganisationStatu.Name);
                    return RedirectToAction("details", new { id = form.Id });
                }

                ControllerMessage.Error(this, "Fill form correct");
                return RedirectToAction("details", new { id = ViewModel.StatusForm.Id });
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                ControllerMessage.Error(this, "System error! Contact us if this is persistance");
                return RedirectToAction("Index");
            }
        }
    }
}
