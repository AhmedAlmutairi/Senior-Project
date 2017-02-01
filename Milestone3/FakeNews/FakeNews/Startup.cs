using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FakeNews.Startup))]
namespace FakeNews
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
