using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MvcAuth.Startup))]
namespace MvcAuth
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
