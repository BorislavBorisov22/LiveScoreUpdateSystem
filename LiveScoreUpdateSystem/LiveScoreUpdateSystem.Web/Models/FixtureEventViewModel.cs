using LiveScoreUpdateSystem.Data.Models.FootballFixtures;
using LiveScoreUpdateSystem.Data.Models.FootballFixtures.Enums;
using LiveScoreUpdateSystem.Web.Infrastructure.Contracts;

namespace LiveScoreUpdateSystem.Web.Models
{
    public class FixtureEventViewModel : IMap<FixtureEvent>
    {
        public int Minute { get; set; }

        public FixtureEventType FixtureEventType { get; set; }

        public string EventScore { get; set; }

        public PlayerInfoViewModel InvolvedPlayer { get; set; }
    }
}