using StreetLightingDomain.Models;
using System.Threading.Tasks;

namespace StreetLightingDomain
{
    public interface IAddressLookUpService
    {
        Task<AddressSearchResult> GetAddressByPostCode(string postCode);
    }
}