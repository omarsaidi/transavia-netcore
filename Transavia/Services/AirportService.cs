using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Transavia.Extensions;
using Transavia.Resources;

namespace Transavia.Services
{
    public class AirportService : IAirportService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger _logger;
        private const string AirportsEndpointUrl = "v2/airports";
        public AirportService(IHttpClientFactory clientFactory,
            ILogger<AirportService> logger)
        {
            _httpClient = clientFactory.CreateClient("TRANSAVIA_API_HOST");
            _logger = logger;
        }
        /// <summary>
        /// Retrieve all airports.
        /// </summary>
        /// <returns>All airports</returns>
        public async Task<Airport[]> GetAllAirportsAsync()
        {
            using HttpResponseMessage response = await _httpClient.GetAsync($"{AirportsEndpointUrl}");
            if (response.IsSuccessStatusCode)
            {
                Airport[] airports = await response.Content.ReadAsAsync<Airport[]>();
                return airports;
            }

            _logger.LogError($"Transavia {response.StatusCode} {await response.Content.ReadAsStringAsync()}");
            return null;
        }
    }
}
