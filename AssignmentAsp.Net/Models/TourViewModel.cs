using AssignmentAsp.Net.Attributes;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssignmentAsp.Net.Models
{
    public class TourViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Tour name is required.")]
        [DisplayName("tour name")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Tour address is required.")]
        [DisplayName("tour address")]
        public string? Address { get; set; }

        [Required(ErrorMessage = "Tour image is required.")]
        [DisplayName("tour image")]
        public string? ImageUrl { get; set; }

        [NotMapped]
        [DisplayName("tour image")]
        [MaxFileSize(500)]
        [AllowedFileType(new string[] { ".jpg", ".png", ".jpeg" })]
        public IFormFile? Image { get; set; }

        [Required(ErrorMessage = "Tour rating is required.")]
        [Range(0, 5, ErrorMessage = "Tour rating must be between 0 and 5.")]
        [Column(TypeName = "decimal(18,2)")]
        [DisplayName("tour rating")]
        public decimal? Rating { get; set; }

        [Required(ErrorMessage = "Tour description is required.")]
        [DisplayName("tour description")]
        public string? Description { get; set; }

        public DateTime CreateDate { get; set; } = DateTime.Now;

    }
}
