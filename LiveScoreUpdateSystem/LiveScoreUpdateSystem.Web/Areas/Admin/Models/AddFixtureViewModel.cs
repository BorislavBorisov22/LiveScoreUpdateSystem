using System;

namespace LiveScoreUpdateSystem.Web.Areas.Admin.Models
{
    public class AddFixtureViewModel
    {
        public Guid HomeTeamId { get; set; }

        public string HomeTeamName { get; set; }

        public Guid AwayTeamId { get; set; }

        public string AwayTeamName { get; set; }
    }
}