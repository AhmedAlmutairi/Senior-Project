using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TheWall.Web.Startup))]
namespace TheWall.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
