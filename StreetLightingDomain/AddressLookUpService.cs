using StreetLightingExternalDependencies.Services;
using System.Threading.Tasks;

namespace StreetLightingDomain
{
    public class AddressLookUpService : IAddressLookUpService
    {
        private readonly IAddressDataExternalService _addressDataExternalService;
        public AddressLookUpService(IAddressDataExternalService addressDataExternalService)
        {
            _addressDataExternalService = addressDataExternalService;
        }

        public async Task GetAddressByPostCode(string postCode)
        {
            var addresses = await _addressDataExternalService.GetAddressByPostCode(postCode);
        }
    }
}
