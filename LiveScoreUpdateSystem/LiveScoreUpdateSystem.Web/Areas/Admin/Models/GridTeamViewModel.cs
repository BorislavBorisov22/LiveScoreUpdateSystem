using AutoMapper;
using LiveScoreUpdateSystem.Common;
using LiveScoreUpdateSystem.Data.Models.FootballFixtures;
using LiveScoreUpdateSystem.Web.Infrastructure.Contracts;
using System;
using System.ComponentModel.DataAnnotations;

namespace LiveScoreUpdateSystem.Web.Areas.Admin.Models
{
    public class GridTeamViewModel : IMap<Team>, ICustomMapping
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [StringLength(GlobalConstants.MaxTeamNameLength, ErrorMessage = "Invalid team name length", MinimumLength = GlobalConstants.MinTeamNameLength)]
        [RegularExpression(GlobalConstants.AlphaNumericalPattern)]
        public string Name { get; set; }

        [Required]
        [RegularExpression(GlobalConstants.AlphaNumericalPattern)]
        public string LeagueName { get; set; }

        public string LeagueFlagPictureUrl { get; set; }

        [Required]
        public string LogoUrl { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Team, GridTeamViewModel>()
              .ForMember(c => c.LeagueName, opt => opt.MapFrom(c => c.League.Name))
              .ForMember(c => c.LeagueFlagPictureUrl, opt => opt.MapFrom(c => c.League.Country.FlagPictureUrl));
        }
    }
}