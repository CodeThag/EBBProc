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
        }
    }
}
