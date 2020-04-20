using AutoMapper;
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
        private readonly Mock<IMapper> _mockMapper;


        public StreetlightingControllerTests()
        {
            _mockStreetLightingDataService = new Mock<IStreetLightingDataService>();
            _mockStreetLightingDataService.Setup(m => m.SaveSurveyResponse(It.IsAny<SurveyDetails>()));

            _mockMapper = new Mock<IMapper>();

            _streetLightingController = new StreetLightingController(_mockStreetLightingDataService.Object, _mockMapper.Object);
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
