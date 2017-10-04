using LiveScoreUpdateSystem.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace LiveScoreUpdateSystem.Web.Areas.Admin.Models
{
    public class LeagueViewModel
    {
        public IEnumerable<SelectListItem> CountriesSelectList { get; set; }

        [Required]
        public CountryViewModel Country { get; set; }

        [Required]
        [MinLength(GlobalConstants.MinLeagueNameLength)]
        [MaxLength(GlobalConstants.MaxLeagueNameLength)]
        public string Name { get; set; }
    }
}