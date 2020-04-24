using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using StreetLighting.Controllers;
using StreetLighting.Models;
using StreetLightingDal;
using StreetLightingDomain;
using StreetLightingDomain.Models;
using Xunit;

namespace StreetLightingXUnitTests
{
    public class StreetlightingControllerTests
    {
        public class StreetlightingControllerPositiveTests
        {
            Mock<IStreetLightingDataService> _mockStreetLightingDataService;
            Mock<IAddressLookUpService> _mockAddressLookUpService;

            StreetLightingController _streetLightingController;
            private readonly Mock<IMapper> _mockMapper;


            public StreetlightingControllerPositiveTests()
            {
                _mockStreetLightingDataService = new Mock<IStreetLightingDataService>();
                _mockStreetLightingDataService.Setup(m => m.SaveSurveyResponse(It.IsAny<SurveyDetails>()));

                _mockAddressLookUpService = new Mock<IAddressLookUpService>();

                _mockMapper = new Mock<IMapper>();

                _streetLightingController = new StreetLightingController(
                    _mockStreetLightingDataService.Object, 
                    _mockAddressLookUpService.Object, 
                    _mockMapper.Object);
            }


            [Fact]
            public void WhenCheckAnswersInvoked_WithSurveyData_ThenFinishViewReturnedOk()
            {
                var answers = new RespondentAnswers
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

                var result = _streetLightingController.CheckAnswers(answers, "stubVal") as ViewResult;

                Assert.Equal("Finish", result.ViewName);
            }
        }

    }
}
