using LiveScoreUpdateSystem.Common;
using LiveScoreUpdateSystem.Data.Models.FootballFixtures.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace LiveScoreUpdateSystem.Web.Areas.LiveUpdater.Models
{
    public class UpdateFixtureViewModel
    {
        public IEnumerable<SelectListItem> Players { get; set; }

        public Guid TeamId { get; set; }

        public Guid Id { get; set; }

        public string TeamName { get; set; }

        [Required]
        public Guid PlayerId { get; set; }

        [Required]
        public FixtureEventType FixtureEvent { get; set; }

        [Range(GlobalConstants.MinFixtureEventMinuteValue, GlobalConstants.MaxFixtureEventMinute)]
        [Required]
        public int Minute { get; set; }
    }
}