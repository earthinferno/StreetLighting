using AutoMapper;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using StreetLightingExternalDependencies.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace StreetLightingExternalDependencies.Services
{
    public class PostcodesAddressData : IAddressDataExternalService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        public PostcodesAddressData(IHttpClientFactory clientFactory, IMapper mapper, IConfiguration configuration)
        {
            _clientFactory = clientFactory;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<Addresses> GetAddressByPostCode(string postCode)
        {
            var apiKey = _configuration["postCodeService_key"];
            var request = new HttpRequestMessage(HttpMethod.Get,
                $"https://api.getAddress.io/find/{postCode}?api-key={apiKey}&expand=true");

            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                string responseStream = await response.Content.ReadAsStringAsync();
                var postCodeLocationData = JsonConvert.DeserializeObject<PostcodesLocation>(responseStream);
                return UpdateAddresses(_mapper.Map<Addresses>(postCodeLocationData), postCode);

            }
            else
            {
                return new Addresses
                {
                    AddressList = new List<Address>()
                };
            }
        }

        // Hack to solve fact I couldn't do this with AutoMapper
        // Refactor to do this in AutoMapper when have more time/wisdom
        public Addresses UpdateAddresses(Addresses addresses, string postCode)
        {
            addresses.AddressList = addresses.AddressList.Select(a => { a.PostCode = postCode; return a; }).ToList();
            return addresses;
        }


    }
}
