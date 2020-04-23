using Microsoft.EntityFrameworkCore;
using StreetLightingDal.Data;
using Xunit;
using Moq;
using StreetLightingDal.Models;
using StreetLightingDal;
using System;
using FluentAssertions;
using AutoMapper;

namespace StreetLightingXUnitTests
{
    public class StreetLightingDataServiceTests
    {
        public class StreetLightingDataServicePositiveTests
        {
            private readonly SurveyDetails _surveyDetails;

            private readonly Mock<DbSet<Respondent>> _mockRespondentDBSet;
            private readonly Mock<StreetLightingDBContext> _mockStreetLightingDBContext;
            private readonly Mock<IMapper> _mockMapper;
            private readonly IStreetLightingDataService _streetLightingDataService;

            public StreetLightingDataServicePositiveTests()
            {
                _surveyDetails = new SurveyDetails
                {
                    Name = "nameStub",
                    EmailAddress = "email@Address.Stub",
                    Address = new SurveyAddress
                    {
                        AddressLine1 = "addressLine1Stub",
                        AddressLine2 = null,
                        City = "cityStub",
                        PostCode = "postCodeStub"
                    },
                    Satisfied = true,
                    Brightness = 1
                };

                _mockRespondentDBSet = new Mock<DbSet<Respondent>>();
                _mockStreetLightingDBContext = new Mock<StreetLightingDBContext>();
                _mockStreetLightingDBContext.Setup(m => m.Respondent).Returns(_mockRespondentDBSet.Object);
                _mockMapper = new Mock<IMapper>();
                _mockMapper.Setup(m => m.Map<Respondent>(_surveyDetails)).Returns(It.IsAny<Respondent>());
                _streetLightingDataService = new StreetLightingDataService(_mockStreetLightingDBContext.Object, _mockMapper.Object);
            }

            [Fact]
            public void OnSaveSurveyResponse_SurveyData_SavedOk()
            {
                _streetLightingDataService.SaveSurveyResponse(_surveyDetails);
                // Refactor service - use an external method/object to do the data translation.
                _mockRespondentDBSet.Verify(m => m.Add(It.IsAny<Respondent>()), Times.Once);
                _mockStreetLightingDBContext.Verify(m => m.SaveChanges(), Times.Once);
            }
        }

        public class StreetLightingDataServiceNegativeTests
        {
            private readonly SurveyDetails _surveyDetails;

            private readonly Mock<DbSet<Respondent>> _mockRespondentDBSet;
            private readonly Mock<StreetLightingDBContext> _mockStreetLightingDBContext;
            private readonly IStreetLightingDataService _streetLightingDataService;
            private readonly Mock<IMapper> _mockMapper;

            public StreetLightingDataServiceNegativeTests()
            {
                _surveyDetails = new SurveyDetails
                {
                    Name = "nameStub",
                    EmailAddress = "email@Address.Stub",
                    Address = new SurveyAddress
                    {
                        AddressLine1 = "addressLine1Stub",
                        AddressLine2 = null,
                        City = "cityStub",
                        PostCode = "postCodeStub"
                    },
                    Satisfied = true,
                    Brightness = 1
                };

                _mockRespondentDBSet = new Mock<DbSet<Respondent>>();
                _mockStreetLightingDBContext = new Mock<StreetLightingDBContext>();
                _mockStreetLightingDBContext.Setup(m => m.Respondent).Throws(new Exception("MockedException"));
                _mockMapper = new Mock<IMapper>();
                _mockMapper.Setup(m => m.Map<Respondent>(_surveyDetails)).Returns(It.IsAny<Respondent>());
                _streetLightingDataService = new StreetLightingDataService(_mockStreetLightingDBContext.Object, _mockMapper.Object);
            }

            [Fact]
            public void OnSaveSurveyResponse_SurveyData_SavedOk()
            {
                Action act = () => _streetLightingDataService.SaveSurveyResponse(_surveyDetails);
                act.Should().Throw<Exception>()
                    .WithMessage("MockedException");
            }
        }
    }
}
