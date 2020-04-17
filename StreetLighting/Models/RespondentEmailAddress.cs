
using System.ComponentModel.DataAnnotations;

namespace StreetLighting.Models
{
    public class RespondentEmailAddress
    {
        [Required]
        public string EmailAddress { get; set; }
    }
}
