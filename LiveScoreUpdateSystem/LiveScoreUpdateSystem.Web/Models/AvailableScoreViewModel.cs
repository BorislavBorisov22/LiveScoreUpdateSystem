using LiveScoreUpdateSystem.Data.Models.FootballFixtures;
using LiveScoreUpdateSystem.Web.Infrastructure.Contracts;
using System;
using AutoMapper;
using LiveScoreUpdateSystem.Data.Models.FootballFixtures.Enums;

namespace LiveScoreUpdateSystem.Web.Models
{
    public class AvailableScoreViewModel : IMap<Fixture>, ICustomMapping
    { 
        public Guid Id { get; set; }

        public int ScoreHomeTeam { get; set; }

        public int ScoreAwayTeam { get; set; }

        public DateTime? FirstHalfStart { get; set; }

        public DateTime? SecondHalfStart { get; set; }

        public string HomeTeamName { get; set; }

        public string HomeTeamLogo { get; set; }

        public string AwayTeamName { get; set; }

        public string AwayTeamLogo { get; set; }

        public string CountryName { get; set; }

        public string CountryFlagUrl { get; set; }

        public string LeagueName { get; set; }

        public FixtureStatus Status { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Fixture, AvailableScoreViewModel>()
            .ForMember(c => c.LeagueName, opt => opt.MapFrom(c => c.HomeTeam.League.Name))
            .ForMember(c => c.HomeTeamName, opt => opt.MapFrom(c => c.HomeTeam.Name))
            .ForMember(c => c.HomeTeamLogo, opt => opt.MapFrom(c => c.HomeTeam.LogoUrl))
            .ForMember(c => c.AwayTeamName, opt => opt.MapFrom(c => c.AwayTeam.Name))
            .ForMember(c => c.AwayTeamLogo, opt => opt.MapFrom(c => c.AwayTeam.LogoUrl))
            .ForMember(c => c.CountryName, opt => opt.MapFrom(c => c.HomeTeam.League.Country.Name))
            .ForMember(c => c.CountryFlagUrl, opt => opt.MapFrom(c => c.HomeTeam.League.Country.FlagPictureUrl));
        }
    }
}