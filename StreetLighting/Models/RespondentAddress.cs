using System.ComponentModel.DataAnnotations;

namespace StreetLighting.Models
{
    public class RespondentAddress
    {
        [Required]
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string PostCode { get; set; }
    }
}
