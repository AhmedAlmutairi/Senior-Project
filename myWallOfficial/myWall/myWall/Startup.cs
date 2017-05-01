using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(myWall.Startup))]
namespace myWall
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}
