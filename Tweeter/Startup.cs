using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Tweeter.Startup))]
namespace Tweeter
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
