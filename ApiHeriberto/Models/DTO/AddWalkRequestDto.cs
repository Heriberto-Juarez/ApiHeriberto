using System.ComponentModel.DataAnnotations;

namespace ApiHeriberto.Models.DTO
{
    public class AddWalkRequestDto
    {
        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string Name { get; set; }
        
        [Required]
        [MaxLength(1000)]
        public string Description { get; set; }
        
        [Required]
        [Range(0,100)]
        public double LengthInKm { get; set; }
        
        [Url]
        public string? WalkImageUrl { get; set; }

        [Required]
        public Guid DifficultyId { get; set; }
        [Required]
        public Guid RegionId { get; set; }
    }
}