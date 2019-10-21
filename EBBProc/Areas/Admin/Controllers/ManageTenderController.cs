using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EBBProc.Areas.Admin.Controllers
{
    public class ManageTenderController : Controller
    {
        // GET: Admin/ManageTender
        public ActionResult Index()
        {
            return View();
        }

        // GET: Admin/ManageTender/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/ManageTender/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/ManageTender/Create
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

        // GET: Admin/ManageTender/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/ManageTender/Edit/5
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

        // GET: Admin/ManageTender/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/ManageTender/Delete/5
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
