using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LiveScoreUpdateSystem.Web.Startup))]
namespace LiveScoreUpdateSystem.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
