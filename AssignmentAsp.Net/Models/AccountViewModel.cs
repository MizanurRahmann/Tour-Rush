using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssignmentAsp.Net.Models
{
    public class AccountViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage="Username is required.")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string? Password { get; set; }
        
        [NotMapped]
        public string? LoginErrorMessage { get; set; }
    }
}
