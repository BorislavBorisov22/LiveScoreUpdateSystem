using LiveScoreUpdateSystem.Data.Models.Abstraction;

namespace LiveScoreUpdateSystem.Data.Models.FootballFixtures
{
    public class Country : DataModel
    {
        public string FlagPictureUrl { get; set; }

        public string Name { get; set; }
    }
}