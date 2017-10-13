using LiveScoreUpdateSystem.Data.Models.FootballFixtures;
using LiveScoreUpdateSystem.Web.Infrastructure.Contracts;

namespace LiveScoreUpdateSystem.Web.Models
{
    public class ListTeamsViewModel : IMap<Team>
    {
        public string Name { get; set; }

        public string LogoUrl { get; set; }

        public bool IsChecked { get; set; }
    }
}