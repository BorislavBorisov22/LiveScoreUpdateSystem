using LiveScoreUpdateSystem.Common;
using LiveScoreUpdateSystem.Data.Models.FootballFixtures;
using LiveScoreUpdateSystem.Data.Models.FootballFixtures.Enums;
using LiveScoreUpdateSystem.Web.Infrastructure.Contracts;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace LiveScoreUpdateSystem.Web.Areas.Admin.Models
{
    public class PlayerViewModel : IMap<Player>
    {
        public IEnumerable<IGrouping<string, TeamViewModel>> LeagueGroupedTeams{ get; set; }

        public IEnumerable<SelectListItem> CountriesList { get; set; }

        [Required]
        [StringLength(GlobalConstants.MaxPlayerNameLength, ErrorMessage = "Invalid first name length", MinimumLength = GlobalConstants.MinPlayerNameLength)]
        [RegularExpression(GlobalConstants.AlphaNumericalPattern)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(GlobalConstants.MaxPlayerNameLength, MinimumLength = GlobalConstants.MinPlayerNameLength)]
        [RegularExpression(GlobalConstants.AlphaNumericalPattern)]
        public string LastName { get; set; }


        [Required]
        [Range(GlobalConstants.MinPlayerAge, GlobalConstants.MaxPlayerAge)]
        public int Age { get; set; }

        [Required]
        [Range(GlobalConstants.MinPlayerShirtNumber, GlobalConstants.MaxPlayerShirtNumber)]
        public int ShirtNumber { get; set; }

        public string PictureUrl { get; set; }

        [Required]
        public PlayerPosition Position { get; set; }

        [Required]
        public string TeamName { get; set; }

        [Required]
        public string CountryName { get; set; }
    }
}