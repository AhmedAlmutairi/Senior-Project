using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(myWall.Web.Startup))]
namespace myWall.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
