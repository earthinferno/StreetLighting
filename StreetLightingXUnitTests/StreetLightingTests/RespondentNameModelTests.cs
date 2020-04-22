using StreetLighting.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace StreetLightingXUnitTests.StreetLightingTests
{
    public class RespondentNameModelTests
    {
        [Fact]
        public void WhenNameSubmitted_AndValueIsNull_ThenModelIsInvalid()
        {
            var respondentName = new RespondentName
            {
            };

            var validationContext = new ValidationContext(respondentName, null, null);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(respondentName, validationContext, validationResults);

            Assert.True(validationResults.Count == 1);
            Assert.Equal("The FullName field is required.", validationResults[0].ErrorMessage);
        }

        [Fact]
        public void WhenNameSubmitted_WithStringLengthOf51_ThenReturnsCurrentViewAsExpected()
        {
            var respondentName = new RespondentName
            {
                FullName = "stubVal"
            };

            var validationContext = new ValidationContext(respondentName, null, null);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(respondentName, validationContext, validationResults);
        }
    }
}
