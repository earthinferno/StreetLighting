using Microsoft.AspNetCore.Mvc;
using Moq;
using StreetLighting.Controllers;
using StreetLighting.Models;
using StreetLightingDomain;
using StreetLightingDomain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace StreetLightingXUnitTests
{
    public class StreetlightingControllerTests
    {
        Mock<IStreetLightingDataService> _mockStreetLightingDataService;

        StreetLightingController _streetLightingController;


        public StreetlightingControllerTests()
        {
            _mockStreetLightingDataService = new Mock<IStreetLightingDataService>();

            _mockStreetLightingDataService.Setup(m => m.SaveSurveyResponse(It.IsAny<SurveyDetails>()));

            _streetLightingController = new StreetLightingController(_mockStreetLightingDataService.Object);
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
                    HouseName = "nameStub",
                    HouseNumber = null,
                    Street = "streetStub",
                    City = "cityStub",
                    PostCode = "postCodeStub"
                },
                Satisfied = "yes",
                Brightness = "1"
            };

            var result = _streetLightingController.CheckAnswers(answers, "stubVal") as ViewResult;

            Assert.Equal("Finish", result.ViewName);
        }
    }
}
