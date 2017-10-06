using AutoMapper;
using LiveScoreUpdateSystem.Data.Models.FootballFixtures;
using LiveScoreUpdateSystem.Web.Infrastructure.Contracts;

namespace LiveScoreUpdateSystem.Web.Areas.Admin.Models
{
    public class TeamAddPlayerViewModel : IMap<Team>, ICustomMapping
    {
        public string Name { get; set; }

        public string LeagueName { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Team, TeamAddPlayerViewModel>()
                .ForMember(t => t.Name, opt => opt.MapFrom(t => t.League.Name));
        }
    }
}