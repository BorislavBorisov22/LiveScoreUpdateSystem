using LiveScoreUpdateSystem.Data.Models.FootballFixtures;
using LiveScoreUpdateSystem.Data.Models.FootballFixtures.Enums;
using LiveScoreUpdateSystem.Web.Infrastructure.Contracts;
using System.Collections.Generic;
using AutoMapper;
using System;

namespace LiveScoreUpdateSystem.Web.Models
{
    public class ScoreDetailsViewModel : IMap<Fixture>, ICustomMapping
    {
        public string HomeTeamName { get; set; }

        public string AwayTeamName { get; set; }

        public string HomeTeamLogo { get; set; }

        public string AwayTeamLogo { get; set; }

        public int ScoreHomeTeam { get; set; }

        public int ScoreAwayTeam { get; set; }

        public string LeagueName { get; set; }

        public DateTime GameDate { get; set; }

        public FixtureStatus Status { get; set; }

        public ICollection<FixtureEventViewModel> FixtureEvents { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Fixture, ScoreDetailsViewModel>()
                .ForMember(c => c.LeagueName, opt => opt.MapFrom(c => c.HomeTeam.League.Name))
                .ForMember(c => c.HomeTeamName, opt => opt.MapFrom(c => c.HomeTeam.Name))
                .ForMember(c => c.AwayTeamName, opt => opt.MapFrom(c => c.AwayTeam.Name))
                .ForMember(c => c.HomeTeamLogo, opt => opt.MapFrom(c => c.HomeTeam.LogoUrl))
                .ForMember(c => c.GameDate, opt => opt.MapFrom(c => c.FirstHalfStart))
                .ForMember(c => c.AwayTeamLogo, opt => opt.MapFrom(c => c.AwayTeam.LogoUrl));
        }
    }
}