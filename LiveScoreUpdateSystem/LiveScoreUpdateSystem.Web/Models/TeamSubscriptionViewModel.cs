using AutoMapper;
using LiveScoreUpdateSystem.Data.Models.FootballFixtures;
using LiveScoreUpdateSystem.Web.Infrastructure.Contracts;
using System;

namespace LiveScoreUpdateSystem.Web.Models
{
    public class TeamSubscriptionViewModel : IMap<Team>, ICustomMapping
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string LogoUrl { get; set; }

        public string LeagueCountryFlagUrl { get; set; }

        public string LeagueName { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Team, TeamSubscriptionViewModel>()
                .ForMember(c => c.LeagueName, opt => opt.MapFrom(c => c.League.Name))
                .ForMember(c => c.LeagueCountryFlagUrl, opt => opt.MapFrom(c => c.League.Country.FlagPictureUrl));
        }
    }
}