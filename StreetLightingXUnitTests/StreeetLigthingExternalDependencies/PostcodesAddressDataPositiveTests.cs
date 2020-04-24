using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Moq;
using Moq.Protected;
using StreetLightingDomain;
using StreetLightingExternalDependencies.Models;
using StreetLightingExternalDependencies.Services;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace StreetLightingXUnitTests.StreeetLigthingExternalDependencies
{
    public class PostcodesAddressDataPositiveTests
    {
        private readonly Mock<IHttpClientFactory> _mockClientFactory;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IConfiguration> _mockConfiguration;

        private readonly PostcodesLocation _postcodesLocation;
        private readonly Addresses _addresses;

        //sut
        private readonly IAddressDataExternalService _postcodesAddressData;

        public PostcodesAddressDataPositiveTests()
        {
            var stringContent = new StringContent(
                "{'postcode':'PostCodeStub', 'latitude':'0.11111', 'longitude':'0.000009', 'addresses': [{'line_1':'AddressLine1Stub', 'line_2':'AddressLine2Stub','TownOrCity':'CityStub'}]}"
                );

            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = stringContent,
                });

            var httpClient = new HttpClient(mockHttpMessageHandler.Object);
            _mockClientFactory = new Mock<IHttpClientFactory>();
            _mockClientFactory.Setup(m => m.CreateClient(It.IsAny<string>())).Returns(httpClient);

            _postcodesLocation = new PostcodesLocation
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

            _addresses = new Addresses
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

            _mockMapper = new Mock<IMapper>();
            // I've cheated here - to uncomment below need to mock the extension method on HttpResponseMessage - ReadAsStringAsync 
            //_mockMapper.Setup(m => m.Map<Addresses>(_postcodesLocation)).Returns(_addresses);
            _mockMapper.Setup(m => m.Map<Addresses>(It.IsAny<PostcodesLocation>())).Returns(_addresses);

            _mockConfiguration = new Mock<IConfiguration>();
            _mockConfiguration.Setup(m => m[It.IsAny<string>()]).Returns("mockApiKey");

            _postcodesAddressData = new PostcodesAddressData(_mockClientFactory.Object, _mockMapper.Object, _mockConfiguration.Object);
        }

        [Fact]
        public async Task WhenValidPostcode_ThenAddressDataReturnedAsExpected()
        {
            var expected = new Addresses
            {
                AddressList = new List<Address>
                {
                    new Address
                    {
                        AddressLine1 = "AddressLine1Stub",
                        AddressLine2 = "AddressLine2Stub",
                        City = "CityStub",
                        PostCode = "PostCodeStub"
                    }
                }
            };

            var result = await _postcodesAddressData.GetAddressByPostCode("PostCodeStub");

            result.Should().BeEquivalentTo(expected);
        }
    }

    public class PostcodesAddressDataNegativeTests
    {
        private readonly Mock<IHttpClientFactory> _mockClientFactory;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IConfiguration> _mockConfiguration;

        private readonly IAddressDataExternalService _postcodesAddressData;

        public PostcodesAddressDataNegativeTests()
        {

            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Content = new StringContent("{'Message':'Not Found' }"),
                });

            var httpClient = new HttpClient(mockHttpMessageHandler.Object);
            _mockClientFactory = new Mock<IHttpClientFactory>();
            _mockClientFactory.Setup(m => m.CreateClient(It.IsAny<string>())).Returns(httpClient);

            _mockMapper = new Mock<IMapper>();

            _mockConfiguration = new Mock<IConfiguration>();
            _mockConfiguration.Setup(m => m[It.IsAny<string>()]).Returns("mockApiKey");


            _postcodesAddressData = new PostcodesAddressData(_mockClientFactory.Object, _mockMapper.Object, _mockConfiguration.Object);
        }

        [Fact]
        public async Task WhenPostcodeServiceIsNotSuccessfull_ThenEmptyAddressListIsReturned()
        {
            var expected = new Addresses
            {
                AddressList = new List<Address>()
            };

            var result = await _postcodesAddressData.GetAddressByPostCode("StubPostCode");

            result.Should().BeEquivalentTo(expected);
        }
    }
}






