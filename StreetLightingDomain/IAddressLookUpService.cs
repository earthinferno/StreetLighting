using System.Threading.Tasks;

namespace StreetLightingDomain
{
    public interface IAddressLookUpService
    {
        Task GetAddressByPostCode(string postCode);
    }
}