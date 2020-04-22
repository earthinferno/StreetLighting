using System.ComponentModel.DataAnnotations;

namespace StreetLighting.Models
{
    public class RespondentName
    {
        [Required]
        [StringLength(50, ErrorMessage = "The {0} value cannot exceed {1} characters. ")]
        public string FullName { get; set; }
    }
}
