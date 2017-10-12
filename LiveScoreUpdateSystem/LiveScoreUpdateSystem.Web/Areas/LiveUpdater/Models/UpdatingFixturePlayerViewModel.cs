using LiveScoreUpdateSystem.Data.Models.FootballFixtures;
using LiveScoreUpdateSystem.Web.Infrastructure.Contracts;
using System;

namespace LiveScoreUpdateSystem.Web.Areas.LiveUpdater.Models
{
    public class UpdatingFixturePlayerViewModel : IMap<Player>
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int ShirtNumber { get; set; }
    }
}