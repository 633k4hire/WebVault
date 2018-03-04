using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebVault.Startup))]
namespace WebVault
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
