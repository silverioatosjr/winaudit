using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TechHelperPOC.Web.Startup))]
namespace TechHelperPOC.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
