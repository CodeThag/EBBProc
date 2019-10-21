using EBBProc.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;

namespace EBBProc.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (UserHelper.IsSystemAdmin())
            {
                return RedirectToAction("index", "dashboard", new { area = "admin" });
            }

            return RedirectToAction("index", "organisation", new { Area = "me"});
        }

        private void isAdmin(IPrincipal user)
        {
            throw new NotImplementedException();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}