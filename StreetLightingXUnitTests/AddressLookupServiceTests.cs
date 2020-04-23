using Moq;
using StreetLightingDomain;
using StreetLightingExternalDependencies.Models;
using StreetLightingExternalDependencies.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace StreetLightingXUnitTests
{
    public class AddressLookupServiceTests
    {
        //private readonly Mock<IHttpClientFactory> _mockClientFactory;
        private readonly Mock<IAddressDataExternalService> _mockAddressDataExternalService;

        private readonly IAddressLookUpService _addressLookUpService;

        public AddressLookupServiceTests()
        {
            _mockAddressDataExternalService = new Mock<IAddressDataExternalService>();


            //var httpClient = new HttpClient();
            //_mockClientFactory.Setup(m => m.CreateClient(It.IsAny<string>())).Returns(httpClient);

            var mockAddressData = new Addresses
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
            _mockAddressDataExternalService.Setup(m => m.GetAddressByPostCode(It.IsAny<string>())).ReturnsAsync(mockAddressData);

            _addressLookUpService = new AddressLookUpService(_mockAddressDataExternalService.Object);
        }

        [Fact]
        public async Task methodGet()
        {
            await _addressLookUpService.GetAddressByPostCode("NG15 8HN");


        }
    }
}
