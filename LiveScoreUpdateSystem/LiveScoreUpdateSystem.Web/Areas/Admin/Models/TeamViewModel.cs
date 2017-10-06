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
        [MinLength(GlobalConstants.MinTeamNameLength)]
        [MaxLength(GlobalConstants.MaxTeamNameLength)]
        [RegularExpression(GlobalConstants.LettersMatchingPattern)]
        public string Name { get; set; }

        [Required]
        public League League { get; set; }

        public ICollection<Player> Players { get; set; }
    }
}