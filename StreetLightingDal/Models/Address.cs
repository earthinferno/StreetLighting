namespace StreetLightingDal.Models
{
    public class Address
    {
        public int AddressId { get; set; }
        public int? HouseNumber { get; set; }
        public string HouseName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
        public int RespondentId { get; set; }
        public virtual Respondent Respondent { get; set; }
    }
}
