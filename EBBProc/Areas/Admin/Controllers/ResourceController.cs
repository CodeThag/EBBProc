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
    public class ResourceController : Controller
    {
        EBBPEntities db = new EBBPEntities();

        // GET: Admin/Resource
        public ActionResult Index()
        {
            try
            {
                ResourceViewModel ViewModel = new ResourceViewModel();
                ViewModel.Resources = db.Resources.ToList();
                return View(ViewModel);
            }
            catch (Exception e)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
                ControllerMessage.Error(this, SystemSettings.GetStringValue("SystemError"));
                return RedirectToAction("Index", "Dashboard");
            }
        }

        // GET: Admin/Resource/Details/5
        public ActionResult Details(Guid id)
        {
            try
            {
                var resource = db.Resources.Find(id);
                if (null == resource)
                {
                    ControllerMessage.Error(this, "Invalid resource!");
                    return RedirectToAction("Index");
                }

                ResourceViewModel ViewModel = new ResourceViewModel();
                ViewModel.Resource = resource;
                return View(resource);
            }
            catch (Exception e)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
                ControllerMessage.Error(this, SystemSettings.GetStringValue("SystemError"));
                return RedirectToAction("Index", "Dashboard");
            }
        }

        // GET: Admin/Resource/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Resource/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here


                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Resource/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/Resource/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Resource/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/Resource/Delete/5
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
