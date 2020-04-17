using System.ComponentModel.DataAnnotations;

namespace StreetLighting.Models
{
    public class LightingResponse
    {
        [Required]
        public bool? Satisfied { get; set; }
    }
}
