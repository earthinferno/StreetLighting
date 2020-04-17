using System.ComponentModel.DataAnnotations;

namespace StreetLighting.Models
{
    public class RespondentAddress
    {
        public int HouseNumber { get; set; }
        public string HouseName { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string PostCode { get; set; }
    }
}
