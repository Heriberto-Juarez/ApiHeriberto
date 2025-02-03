using System.ComponentModel.DataAnnotations;

namespace ApiHeriberto.Models.DTO
{
    public class UpdateRegionDto
    {
        [Required]
        [MinLength(3)]
        [MaxLength(3)]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
