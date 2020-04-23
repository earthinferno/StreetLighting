using AutoMapper;
using FluentAssertions;
using StreetLightingExternalDependencies.Mappers;
using StreetLightingExternalDependencies.Models;
using System.Collections.Generic;
using Xunit;

namespace StreetLightingXUnitTests.StreeetLigthingExternalDependencies.Mappers
{
    public class AddressDataExternalServiceMapperTests
    {
        IMapper _mapper;

        public AddressDataExternalServiceMapperTests()
        {
            var config = new MapperConfiguration(mc => mc.AddProfile(new AddressDataExternalServiceMapper()));
            _mapper = config.CreateMapper();

        }

        [Fact]
        public void test1()
        {
            var expected = new Addresses
            {
                AddressList = new List<Address>
                {
                    new Address
                    {
                        AddressLine1 = "AddressLine1Stub",
                        AddressLine2 = "AddressLine2Stub",
                        City = "CityStub"
                    }
                }
            };

            var source = new PostcodesLocation
            {
                PostCode = "PostCodeStub",
                Latitude = 0.11111M,
                Longitude = 0.000009M,
                Addresses = new List<PostcodesAddress>
                {
                    new PostcodesAddress
                    {
                        Line1 = "AddressLine1Stub",
                        Line2 = "AddressLine2Stub",
                        TownOrCity = "CityStub"
                    }
                }
            };

            var result = _mapper.Map<Addresses>(source);

            result.Should().BeEquivalentTo(expected);

        }
    }
}
