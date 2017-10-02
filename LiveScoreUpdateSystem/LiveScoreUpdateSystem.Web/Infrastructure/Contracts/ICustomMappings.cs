using AutoMapper;

namespace LiveScoreUpdateSystem.Web.Infrastructure.Contracts
{
    public interface ICustomMappings
    {
        void CreateMappings(IMapperConfigurationExpression configuration);
    }
}