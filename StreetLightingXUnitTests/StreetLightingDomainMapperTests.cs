using AutoMapper;
using FluentAssertions;
using StreetLighting.Mappers;
using StreetLighting.Models;
using StreetLightingDomain.Models;
using Xunit;

namespace StreetLightingXUnitTests
{
    public class StreetLightingDomainMapperTests
    {
        IMapper _mapper;
        private readonly RespondentAnswers _respondentAnswers;

        public StreetLightingDomainMapperTests()
        {
            var config = new MapperConfiguration(mc => mc.AddProfile(new StreetLightingMapper()));
            _mapper = config.CreateMapper();

            _respondentAnswers = new RespondentAnswers
            {
                Name = "nameStub",
                EmailAddress = "email@Address.Stub",
                Address = new RespondentAddress
                {
                    AddressLine1 = "nameStub",
                    AddressLine2 = null,
                    City = "cityStub",
                    PostCode = "postCodeStub"
                },
                Satisfied = "yes",
                Brightness = 1
            };
        }


        [Fact]
        public void OnValidRespondentAnswersSurveyDetailsCreatedOkay()
        {
            var expected = new SurveyDetails
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

            var result = _mapper.Map<SurveyDetails>(_respondentAnswers);

            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void OnValidSurveyDetailsRespondentAnswersCreatedOkay()
        {
            var expected = _respondentAnswers;
            var surveyDetails = new SurveyDetails
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

            var result = _mapper.Map<RespondentAnswers>(surveyDetails);

            result.Should().BeEquivalentTo(expected);
        }

    }
}
