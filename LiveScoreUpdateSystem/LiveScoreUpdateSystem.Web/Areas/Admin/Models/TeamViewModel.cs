using LiveScoreUpdateSystem.Common;
using LiveScoreUpdateSystem.Data.Models.FootballFixtures;
using LiveScoreUpdateSystem.Web.Infrastructure.Contracts;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace LiveScoreUpdateSystem.Web.Areas.Admin.Models
{
    public class TeamViewModel : IMap<Team>
    {
        public IEnumerable<SelectListItem> LeaguesSelectList { get; set; }

        [Required]
        [StringLength(GlobalConstants.MaxTeamNameLength, ErrorMessage = "Invalid team name length", MinimumLength = GlobalConstants.MinTeamNameLength)]
        [RegularExpression(GlobalConstants.AlphaNumericalPattern)]
        public string Name { get; set; }

        [Required]
        public string LeagueName { get; set; }

        [Required]
        public string LogoUrl { get; set; }

        public ICollection<Player> Players { get; set; }
    }
}