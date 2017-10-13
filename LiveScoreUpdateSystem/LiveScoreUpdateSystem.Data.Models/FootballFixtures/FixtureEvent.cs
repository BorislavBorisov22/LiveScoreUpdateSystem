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
        [Range(GlobalConstants.MinFixtureEventMinuteValue, int.MaxValue)]
        public int Minute { get; set; }

        public virtual Player InvolvedPlayer { get; set; }
     
        public string EventScore { get; set; }
    }
}
