
using System.ComponentModel.DataAnnotations;

namespace StreetLighting.Models
{
    public class RespondentEmailAddress
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string Email { get; set; }
    }
}
