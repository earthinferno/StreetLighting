namespace StreetLightingDomain.Models
{
    public class SurveyDetails
    {
        public string Name { get; set; }

        public string EmailAddress { get; set; }

        public SurveyAddress Address { get; set; }

        public bool Satisfied { get; set; }

        public int Brightness { get; set; }
    }
}
