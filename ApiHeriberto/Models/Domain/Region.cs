using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiHeriberto.Models.Domain
{
    [Table("Regions")]
    public class Region
    {

        [Key]
        public Guid Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(3)]
        public string Code { get; set; }

        [Required]
        public string Name { get; set; }

        public string? RegionImageUrl { get; set; }

    }
}
