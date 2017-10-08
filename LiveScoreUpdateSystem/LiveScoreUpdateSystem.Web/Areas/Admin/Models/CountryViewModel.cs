using LiveScoreUpdateSystem.Common;
using LiveScoreUpdateSystem.Data.Models.FootballFixtures;
using LiveScoreUpdateSystem.Web.Infrastructure.Contracts;
using System;
using System.ComponentModel.DataAnnotations;

namespace LiveScoreUpdateSystem.Web.Areas.Admin.Models
{
    public class CountryViewModel : IMap<Country>
    {
        public Guid Id { get; set; }

        public string FlagPictureUrl { get; set; }

        [Required(ErrorMessage = "Name is Required")]
        [StringLength(GlobalConstants.MaxCountryNameLength, ErrorMessage = "Invalid Country name length", MinimumLength = GlobalConstants.MinCountryNameLength)]
        public string Name { get; set; }
    }
}