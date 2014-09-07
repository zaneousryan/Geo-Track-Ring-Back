using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Hackathon2.Web.Startup))]
namespace Hackathon2.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
