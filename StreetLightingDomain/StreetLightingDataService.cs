using StreetLightingDomain.Models;
using StreetLightingDal.Data;
using StreetLightingDal.Models;
using System;

namespace StreetLightingDomain
{
    public class StreetLightingDataService : IStreetLightingDataService
    {
        private readonly StreetLightingDBContext _dbContext;
        public StreetLightingDataService(StreetLightingDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void SaveSurveyResponse(SurveyDetails survey)
        {
            var respondent = new Respondent
            {
                Name = survey.Name,
                EmailAddress = survey.EmailAddress,
                Address = new Address
                {
                    HouseNumber = survey.Address.HouseNumber,
                    HouseName = survey.Address.HouseName,
                    Street = survey.Address.Street,
                    City = survey.Address.City,
                    PostCode = survey.Address.PostCode
                },
                QuestionnaireResponse = new Response
                {
                    BrightnessLevel = survey.Brightness,
                    Satisfied = survey.Satisfied
                }
            };

            _dbContext.Respondent.Add(respondent);
            _dbContext.SaveChanges();
        }
    }
}
