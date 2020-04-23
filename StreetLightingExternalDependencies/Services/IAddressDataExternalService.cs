using StreetLightingExternalDependencies.Models;
using System.Threading.Tasks;

namespace StreetLightingExternalDependencies.Services
{
    public interface IAddressDataExternalService
    {
        Task<Addresses> GetAddressByPostCode(string postCode);
    }
}