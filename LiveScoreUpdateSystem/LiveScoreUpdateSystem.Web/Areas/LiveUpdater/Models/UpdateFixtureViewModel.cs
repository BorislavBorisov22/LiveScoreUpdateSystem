using LiveScoreUpdateSystem.Common;
using LiveScoreUpdateSystem.Data.Models.FootballFixtures.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LiveScoreUpdateSystem.Web.Areas.LiveUpdater.Models
{
    public class UpdateFixtureViewModel
    {
        public IEnumerable<string> Players { get; set; }

        [Required]
        public Guid PlayerId { get; set; }

        [Required]
        public FixtureEventType FixtureEvent { get; set; }

        [Range(GlobalConstants.MinFixtureEventMinuteValue, GlobalConstants.MaxFixtureEventMinute)]
        [Required]
        public int Minute { get; set; }
    }
}