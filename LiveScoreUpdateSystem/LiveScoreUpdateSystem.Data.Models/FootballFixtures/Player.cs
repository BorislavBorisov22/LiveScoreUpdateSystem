using LiveScoreUpdateSystem.Data.Models.Abstraction;
using System.ComponentModel.DataAnnotations;

namespace LiveScoreUpdateSystem.Data.Models.FootballFixtures
{
    public class Player : DataModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public int ShirtNumber { get; set; }

        public string PictureUrl { get; set; }

        public virtual Team Team { get; set; }

        public virtual Country Country { get; set; }
    }
}
