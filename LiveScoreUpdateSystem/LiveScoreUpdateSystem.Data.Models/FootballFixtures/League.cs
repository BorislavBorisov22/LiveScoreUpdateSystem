using LiveScoreUpdateSystem.Data.Models.Abstraction;
using System.Collections;
using System.Collections.Generic;

namespace LiveScoreUpdateSystem.Data.Models.FootballFixtures
{
    public class League : DataModel
    {
        public string Name { get; set; }

        public virtual Country Country { get; set; }

        public virtual ICollection<Team> Teams { get; set; }
    }
}
