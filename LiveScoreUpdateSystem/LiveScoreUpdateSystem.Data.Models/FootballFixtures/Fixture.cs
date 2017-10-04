using LiveScoreUpdateSystem.Common;
using LiveScoreUpdateSystem.Data.Models.Abstraction;
using LiveScoreUpdateSystem.Data.Models.FootballFixtures.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LiveScoreUpdateSystem.Data.Models.FootballFixtures
{
    public class Fixture : DataModel
    {
        public Fixture()
        {
            this.ScoreHomeTeam = 0;
            this.ScoreAwayTeam = 0;
            this.Half = Half.First;
            this.Status = FixtureStatus.Pending;
            this.Minute = 0;
        }

        [Required]
        public int ScoreHomeTeam { get; set; }

        [Required]
        public int ScoreAwayTeam { get; set; }

        [Required]
        public Half Half { get; set; }

        [Required]
        public FixtureStatus Status { get; set; }

        [Required]
        [MinLength(GlobalConstants.MinFixtureMinute)]
        [MaxLength(GlobalConstants.MaxFixtureMinute)]
        public int Minute { get; set; }

        [Required]
        public virtual Team HomeTeam { get; set; }

        [Required]
        public virtual Team AwayTeam { get; set; }

        [Required]
        public virtual League League { get; set; }

        [Required]
        public virtual ICollection<FixtureEvent> FixtureEvents { get; set; }
    }
}
