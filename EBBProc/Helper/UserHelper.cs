using EBBProc.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EBBProc.Helper
{
    public class UserHelper
    {
        public static bool IsSystemAdmin()
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var user = HttpContext.Current.User.Identity;
                ApplicationDbContext context = new ApplicationDbContext();
                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var s = UserManager.GetRoles(user.GetUserId());
                if (s[0].ToString() == "Adminstrator")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;

        }

        public static bool IsOrganisationAdmin()
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var user = HttpContext.Current.User.Identity;
                ApplicationDbContext context = new ApplicationDbContext();
                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var s = UserManager.GetRoles(user.GetUserId());
                if (s[0].ToString() == "Organisation Adminstrator")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        public static bool HasProfile()
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var user = HttpContext.Current.User.Identity;
                using (var context = new DAL.EBBPEntities())
                {
                    var userX = context.UserProfiles.Where(x => x.Id == user.GetUserId()).FirstOrDefault();
                    if (null == userX)
                        return false;
                }
            }
            return true;
        }
    }
}