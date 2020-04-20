using System.ComponentModel.DataAnnotations;

namespace StreetLighting.Models
{
    public class RespondentAnswers
    {
        public string Name { get; set; }

        public string EmailAddress { get; set; }

        public RespondentAddress Address { get; set; }

        public string Satisfied { get; set; }

        [Range(1, 10,
        ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int Brightness { get; set; }
    }
}
