using System;
using AutoMapper;
using LiveScoreUpdateSystem.Data.Models.FootballFixtures;
using LiveScoreUpdateSystem.Data.Models.FootballFixtures.Enums;
using LiveScoreUpdateSystem.Web.Infrastructure.Contracts;

namespace LiveScoreUpdateSystem.Web.Models
{
    public class PlayerInfoViewModel : IMap<Player>, ICustomMapping
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PictureUrl { get; set; }

        public int Age { get; set; }

        public PlayerPosition PlayerPosition { get; set; }

        public string TeamName { get; set; }

        public string TeamLogo { get; set; }

        public string CountryName { get; set; }

        public string CountryFlagUrl { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Player, PlayerInfoViewModel>()
              .ForMember(c => c.CountryName, opt => opt.MapFrom(c => c.Country.Name))
              .ForMember(c => c.TeamName, opt => opt.MapFrom(c => c.Team.Name))
              .ForMember(c => c.TeamLogo, opt => opt.MapFrom(c => c.Team.LogoUrl))
              .ForMember(c => c.CountryFlagUrl, opt => opt.MapFrom(c => c.Country.FlagPictureUrl));
        }
    }
}