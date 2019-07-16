using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ShieldWeb.Startup))]
namespace ShieldWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
