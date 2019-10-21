using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EBBProc.Areas.Admin.Controllers
{
    public class NavigationController : Controller
    {
        // GET: Admin/Navigation
        public PartialViewResult Display()
        {
            return PartialView();
        }
    }
}
