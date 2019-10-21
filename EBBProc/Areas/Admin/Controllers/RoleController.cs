using EBBProc.Areas.Admin.Models;
using EBBProc.Helper;
using EBBProc.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EBBProc.Areas.Admin.Controllers
{
    [Authorize]
    public class RoleController : Controller
    {
        private ApplicationDbContext context;
        private RoleManager<IdentityRole> roleManager;
        public RoleController()
        {
            context = new ApplicationDbContext();
            roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
        }

        // GET: Admin/Role
        public ActionResult Index()
        {
            RoleViewModel ViewModel = new RoleViewModel();
            ViewModel.Roles = roleManager.Roles.ToList();
            return View(ViewModel);
        }

        // GET: Admin/Role/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/Role/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Role/Create
        [HttpPost]
        public ActionResult Create(RoleViewModel ViewModel)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    var role = new IdentityRole();
                    role.Name = ViewModel.RoleForm.Name;
                    roleManager.Create(role);

                    ControllerMessage.Success(this, "Role: " + role.Name + " created!");
                    return RedirectToAction("Index");
                }

                ControllerMessage.Error(this, "Invalid submission!");
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                ControllerMessage.Error(this, SystemSettings.GetStringValue("SystemError"));
                return RedirectToAction("Index");
            }
        }

        // GET: Admin/Role/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/Role/Edit/5
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

        // GET: Admin/Role/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/Role/Delete/5
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
