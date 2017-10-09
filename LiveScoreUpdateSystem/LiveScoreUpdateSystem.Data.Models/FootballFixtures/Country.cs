using LiveScoreUpdateSystem.Common;
using LiveScoreUpdateSystem.Data.Models.Abstraction;
using System.ComponentModel.DataAnnotations;

namespace LiveScoreUpdateSystem.Data.Models.FootballFixtures
{
    public class Country : DataModel
    {
        [Required]
        public string FlagPictureUrl { get; set; }

        [Required]
        [MinLength(GlobalConstants.MinCountryNameLength)]
        [MaxLength(GlobalConstants.MaxCountryNameLength)]
        [RegularExpression(GlobalConstants.AlphaNumericalPattern)]
        public string Name { get; set; }
    }
}