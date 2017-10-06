using Bytes2you.Validation;
using LiveScoreUpdateSystem.Web.Infrastructure.Providers.Contracts;
using Ninject;
using System.Web.Mvc;

namespace LiveScoreUpdateSystem.Web.Infrastructure.Providers
{
    public class ServiceLocator : IServiceLocator
    {
        private readonly IKernel kernel;

        static ServiceLocator()
        {
            InstanceProvider = new ServiceLocator(DependencyResolver.Current.GetService<IKernel>());
        }

        public ServiceLocator(IKernel kernel)
        {
            Guard.WhenArgument(kernel, "kernel").IsNull().Throw();

            this.kernel = kernel;
        }

        public static IServiceLocator InstanceProvider { get; set; }

        public T ProvideInstance<T>()
        {
            return this.kernel.Get<T>();
        }
    }
}