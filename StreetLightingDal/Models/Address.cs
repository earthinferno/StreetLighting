namespace StreetLightingDal.Models
{
    public class Address
    {
        public int AddressId { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
        public int RespondentId { get; set; }
        public virtual Respondent Respondent { get; set; }
    }
}
