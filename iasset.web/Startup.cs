using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(iasset.web.Startup))]
namespace iasset.web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
