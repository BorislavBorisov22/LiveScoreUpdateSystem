using LiveScoreUpdateSystem.Data.Models.Abstraction;
using LiveScoreUpdateSystem.Data.Models.FootballFixtures.Enums;
using System.Collections.Generic;

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

        public int ScoreHomeTeam { get; set; }

        public int ScoreAwayTeam { get; set; }

        public Half Half { get; set; }

        public FixtureStatus Status { get; set; }

        public int Minute { get; set; }

        public virtual Team HomeTeam { get; set; }

        public virtual Team AwayTeam { get; set; }

        public virtual ICollection<FixtureEvent> FixtureEvents { get; set; }
    }
}
