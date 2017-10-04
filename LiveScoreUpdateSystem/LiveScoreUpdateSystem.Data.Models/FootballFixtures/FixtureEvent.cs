using LiveScoreUpdateSystem.Common;
using LiveScoreUpdateSystem.Data.Models.Abstraction;
using LiveScoreUpdateSystem.Data.Models.FootballFixtures.Enums;
using System.ComponentModel.DataAnnotations;

namespace LiveScoreUpdateSystem.Data.Models.FootballFixtures
{
    public class FixtureEvent : DataModel
    {
        [Required]
        public FixtureEventType FixtureEventType { get; set; }

        [Required]
        [MinLength(GlobalConstants.MinFixtureEventMinuteValue)]
        public int Minute { get; set; }

        public virtual Player InvolvedPlayer { get; set; }
    }
}
