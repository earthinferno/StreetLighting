using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using StreetLightingDal.Models;
using StreetLightingDomain.Models;
using Xunit;
using FluentAssertions;
using StreetLightingDomain.Mappers;

namespace StreetLightingXUnitTests
{

    public class StreetLightingDomainDalMapperTests
    {
        IMapper _mapper;
        private readonly SurveyDetails _surveyDetails;

        public StreetLightingDomainDalMapperTests()
        {
            var config = new MapperConfiguration(mc => mc.AddProfile(new DalMapper()));
            _mapper = config.CreateMapper();

            _surveyDetails = new SurveyDetails
            {
                Name = "nameStub",
                EmailAddress = "email@Address.Stub",
                Address = new SurveyAddress
                {
                    AddressLine1 = "nameStub",
                    AddressLine2 = null,
                    City = "cityStub",
                    PostCode = "postCodeStub"
                },
                Satisfied = true,
                Brightness = 1
            };
        }

        [Fact]
        public void OnValidSurveyDetailsRespondentCreatedOkay()
        {
            var expected = new Respondent
            {
                Name = _surveyDetails.Name,
                EmailAddress = _surveyDetails.EmailAddress,
                Address = new Address
                {
                    AddressLine1 = _surveyDetails.Address.AddressLine1,
                    AddressLine2 = _surveyDetails.Address.AddressLine2,
                    City = _surveyDetails.Address.City,
                    PostCode = _surveyDetails.Address.PostCode
                },
                QuestionnaireResponse = new Response
                {
                    Satisfied = _surveyDetails.Satisfied,
                    BrightnessLevel = _surveyDetails.Brightness
                }
            };

            var result = _mapper.Map<Respondent>(_surveyDetails);

            result.Should().BeEquivalentTo(expected);
        }
    }
}
