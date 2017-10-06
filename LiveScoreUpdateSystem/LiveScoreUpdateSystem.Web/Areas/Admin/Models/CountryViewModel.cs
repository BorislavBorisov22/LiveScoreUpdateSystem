using LiveScoreUpdateSystem.Common;
using LiveScoreUpdateSystem.Data.Models.FootballFixtures;
using LiveScoreUpdateSystem.Web.Infrastructure.Contracts;
using System.ComponentModel.DataAnnotations;

namespace LiveScoreUpdateSystem.Web.Areas.Admin.Models
{
    public class CountryViewModel : IMap<Country>
    {
        public string FlagPictureUrl { get; set; }

        [Required]
        [MinLength(GlobalConstants.MinCountryNameLength)]
        [MaxLength(GlobalConstants.MaxCountryNameLength)]
        public string Name { get; set; }
    }
}