using System.ComponentModel.DataAnnotations;

namespace ApiHeriberto.Models.DTO
{
    public class AddRegionDto
    {
        [Required]
        [MinLength(3)]
        [MaxLength(3)]
        [RegularExpression("^[A-Z]+$", ErrorMessage ="Solo mayúsculas de la A a la Z son permitidas")]
        public string Code { get; set; }
        [Required]
        [MinLength(3)] // Optional: ensure a minimum length for the Name
        [MaxLength(100)] // Optional: specify a maximum length
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "El nombre solo puede tener letras y espacios")] // Optional: restrict to alphabetic characters and 
        public string Name { get; set; }

        [Url(ErrorMessage = "La URL de la imagen no es válida")]
        public string? RegionImageUrl { get; set; }
    }
}
