using LiveScoreUpdateSystem.Data.Models.FootballFixtures.Enums;
using System;

namespace LiveScoreUpdateSystem.Web.Areas.LiveUpdater.Models
{
    public class UpdateFixtureStatusViewModel
    {
        public Guid Id { get; set; }

        public FixtureStatus Status { get; set; }
    }
}