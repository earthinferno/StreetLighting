using System.ComponentModel.DataAnnotations;

namespace StreetLighting.Models
{
    public class BrightnessResponse
    {
        [Required]
        [Range(1,10)]
        public int Brightness { get; set; }
    }
}
