[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(LiveScoreUpdateSystem.Web.App_Start.DependencyInjectionConfig), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(LiveScoreUpdateSystem.Web.App_Start.DependencyInjectionConfig), "Stop")]

namespace LiveScoreUpdateSystem.Web.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Extensions.Conventions;
    using Ninject.Web.Common;
    using System.Data.Entity;
    using LiveScoreUpdateSystem.Data;
    using LiveScoreUpdateSystem.Data.Repositories;
    using LiveScoreUpdateSystem.Data.Repositories.Contracts;
    using LiveScoreUpdateSystem.Services.Data.Abstraction.Contracts;
    using LiveScoreUpdateSystem.Services.Common.Contracts;
    using LiveScoreUpdateSystem.Data.SaveContext.Contracts;
    using LiveScoreUpdateSystem.Data.Repositories.SaveContext;
    using LiveScoreUpdateSystem.Web.Infrastructure.Filters;
    using LiveScoreUpdateSystem.Web.Infrastructure.Attributes;
    using Ninject.Web.Mvc.FilterBindingSyntax;

    public static class DependencyInjectionConfig
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind(x =>
            {
                x.FromThisAssembly()
                .SelectAllClasses()
                .BindDefaultInterface();
            });

            kernel.Bind(x =>
            {
                x.FromAssemblyContaining(typeof(IDataService<>))
                .SelectAllClasses()
                .BindDefaultInterface();
            });

            kernel.Bind(x =>
            {
                x.FromAssemblyContaining(typeof(IService))
                .SelectAllClasses()
                .BindDefaultInterface();
            });

            kernel
                .Bind(typeof(DbContext), typeof(MsSqlDbContext))
                .To<MsSqlDbContext>()
                .InRequestScope();

            kernel.Bind<ISaveContext>().To<SaveContext>();
            kernel.BindFilter<SaveChangesFilter>(System.Web.Mvc.FilterScope.Controller, 0).WhenActionMethodHas<SaveChangesAttribute>();
            kernel.Bind(typeof(IEfRepository<>)).To(typeof(EfRepository<>)).InSingletonScope();
        }
    }
}
