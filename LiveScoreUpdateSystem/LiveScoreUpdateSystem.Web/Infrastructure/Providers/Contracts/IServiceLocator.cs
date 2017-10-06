namespace LiveScoreUpdateSystem.Web.Infrastructure.Providers.Contracts
{
    public interface IServiceLocator
    {
        T ProvideInstance<T>();
    }
}
