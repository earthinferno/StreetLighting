using System.ComponentModel.DataAnnotations;

namespace StreetLighting.Models
{
    public class RespondentAnswers
    {
        public string Name { get; set; }

        public string EmailAddress { get; set; }

        public RespondentAddress Address { get; set; }

        public string Satisfied { get; set; }
    }
}
