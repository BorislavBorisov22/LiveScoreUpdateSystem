using LiveScoreUpdateSystem.Common;
using LiveScoreUpdateSystem.Data.Models.FootballFixtures;
using LiveScoreUpdateSystem.Web.Infrastructure.Contracts;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace LiveScoreUpdateSystem.Web.Areas.Admin.Models
{
    public class LeagueViewModel : IMap<League>
    {
        public IEnumerable<SelectListItem> CountriesSelectList { get; set; }

        [Required]
        public CountryViewModel Country { get; set; }

        [Required]
        [MinLength(GlobalConstants.MinLeagueNameLength)]
        [MaxLength(GlobalConstants.MaxLeagueNameLength)]
        public string Name { get; set; }

        [Required]
        [Range(GlobalConstants.MinLeagueSeasonValue, GlobalConstants.MaxLeagueSeasonValue)]     
        public int Season { get; set; }
    }
}