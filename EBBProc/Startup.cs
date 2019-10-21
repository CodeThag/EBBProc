using EBBProc.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EBBProc.Startup))]
namespace EBBProc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRolesandUsers();
        }

        private void CreateRolesandUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            if (!roleManager.RoleExists("Adminstrator"))
            {
                var role = new IdentityRole();
                role.Name = "Adminstrator";
                roleManager.Create(role);
            }

            // creating Creating Manager role     
            if (!roleManager.RoleExists("Organisation Adminstrator"))
            {
                var role = new IdentityRole();
                role.Name = "Organisation Adminstrator";
                roleManager.Create(role);
            }
        }
    }
}
