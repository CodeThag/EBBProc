using DAL;
using EBBProc.Areas.Me.Models;
using EBBProc.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EBBProc.Areas.Me.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        EBBPEntities db = new EBBPEntities();


        // GET: Me/Profile
        public ActionResult Index()
        {
            ProfileViewModel ViewModel = new ProfileViewModel();
            AspNetUser user = db.AspNetUsers.Where(x => x.UserName.Equals(User.Identity.Name)).FirstOrDefault();

            UserProfile profile = user.UserProfile;
            if (null != profile)
            {
                var form = new ProfileForm();
                form.Firstname = profile.Firstname;
                form.Surname = profile.Surname;
                form.Profession = profile.Profession;
                ViewModel.ProfileForm = form;
            }
            return View(ViewModel);
        }


        public ActionResult Update(ProfileViewModel ViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var form = ViewModel.ProfileForm;

                    AspNetUser user = db.AspNetUsers.Where(x => x.UserName.Equals(User.Identity.Name)).FirstOrDefault();
                    if (null == user.UserProfile)
                    {
                        user.UserProfile = new UserProfile();
                        user.UserProfile.CreatedBy = User.Identity.Name;
                        user.UserProfile.CreatedDate = DateTime.Now;
                    }

                    user.UserProfile.Firstname = form.Firstname;
                    user.UserProfile.Surname = form.Surname;
                    user.UserProfile.Profession = form.Profession;
                    user.UserProfile.ModifiedBy = User.Identity.Name;
                    user.UserProfile.ModifiedDate = DateTime.Now;

                    db.SaveChanges();
                    ControllerMessage.Success(this, "Profile updated");
                    return RedirectToAction("index");
                }

                ControllerMessage.Error(this, "Please fill form correctly!");
                return RedirectToAction("index");
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                ControllerMessage.Error(this, "System Error");
                return RedirectToAction("index");
            }

        }
    }
}
