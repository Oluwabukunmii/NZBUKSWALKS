using System.ComponentModel.DataAnnotations;
using NZBUKSWALKS.API.Models.Domain;

namespace NZBUKSWALKS.API.Models.DTO
{
    public class AddWalkRequestDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        [MaxLength(1000)]

        public string Description { get; set; }
        [Required]
        [Range(0 , 50)]

        public double LengthInKm { get; set; }

        public string? WalkImageUrl { get; set; }
        [Required]

        //relationships//
        public Guid Difficultyid { get; set; }
        [Required]

        public Guid Regionid { get; set; }

     

    }
}
