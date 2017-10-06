using AutoMapper;

namespace LiveScoreUpdateSystem.Web.Infrastructure.Contracts
{
    public interface ICustomMapping
    {
        void CreateMappings(IMapperConfigurationExpression configuration);
    }
}