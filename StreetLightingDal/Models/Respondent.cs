namespace StreetLightingDal.Models
{
    public class Respondent
    {
        public int RespondentId { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public virtual Address Address { get; set; }
        public virtual Response QuestionnaireResponse { get; set; }
    }
}
