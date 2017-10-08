using LiveScoreUpdateSystem.Common;
using LiveScoreUpdateSystem.Data.Models.Abstraction;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LiveScoreUpdateSystem.Data.Models.FootballFixtures
{
    public class League : DataModel
    {
        [Required]
        [MinLength(GlobalConstants.MinLeagueNameLength)]
        [MaxLength(GlobalConstants.MaxLeagueNameLength)]
        [RegularExpression(GlobalConstants.LettersMatchingPattern)]
        public string Name { get; set; }

        public virtual Country Country { get; set; }

        public virtual ICollection<Team> Teams { get; set; }

        public virtual ICollection<Fixture> Fixtures { get; set; }

        [Required]
        [Range(GlobalConstants.MinLeagueSeasonValue, GlobalConstants.MaxLeagueSeasonValue)]
        public int Season { get; set; }
    }
}
