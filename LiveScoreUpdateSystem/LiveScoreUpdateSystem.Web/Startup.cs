using LiveScoreUpdateSystem.Web.Infrastructure.Providers;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Ninject;
using Owin;

[assembly: OwinStartupAttribute(typeof(LiveScoreUpdateSystem.Web.Startup))]
namespace LiveScoreUpdateSystem.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            var configuration = new HubConfiguration();
            configuration.Resolver = new NinjectSignalRDependencyResolver(ServiceLocator.InstanceProvider.ProvideInstance<IKernel>());
            app.MapSignalR(configuration);
        }
    }
}
