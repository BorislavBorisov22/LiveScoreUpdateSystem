using AutoMapper;
using LiveScoreUpdateSystem.Data.Models.FootballFixtures;
using LiveScoreUpdateSystem.Web.Infrastructure.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace LiveScoreUpdateSystem.Web.Areas.Admin.Models
{
    public class AddFixtureViewModel
    {
        public IEnumerable<string> TeamsNames { get; set; }

        [Required]
        public string HomeTeamName { get; set; }

        [Required]
        public string AwayTeamName { get; set; }

        [Required]
        public DateTime StartTime { get; set; }
    }
}