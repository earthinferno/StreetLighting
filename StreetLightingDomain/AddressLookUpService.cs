using AutoMapper;
using StreetLightingDomain.Models;
using StreetLightingExternalDependencies.Services;
using System.Threading.Tasks;

namespace StreetLightingDomain
{
    public class AddressLookUpService : IAddressLookUpService
    {
        private readonly IAddressDataExternalService _addressDataExternalService;
        private readonly IMapper _mapper;
        public AddressLookUpService(IAddressDataExternalService addressDataExternalService, IMapper mapper)
        {
            _addressDataExternalService = addressDataExternalService;
            _mapper = mapper;
        }

        public async Task<AddressSearchResult> GetAddressByPostCode(string postCode)
        {
            var addresses = await _addressDataExternalService.GetAddressByPostCode(postCode);
            return _mapper.Map<AddressSearchResult>(addresses);
        }
    }
}
