using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Transavia.Extensions;
using Transavia.Resources;

namespace Transavia.Services
{
    public class FlightRouteService : IFlightRouteService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger _logger;
        public FlightRouteService(IHttpClientFactory clientFactory,
            ILogger<FlightRouteService> logger)
        {
            _httpClient = clientFactory.CreateClient("TRANSAVIA_API_HOST");
            _logger = logger;
        }
        /// <summary>
        /// Retrieves all routes.
        /// </summary>
        /// <param name="queryParams">
        /// origin(optional) : IATA-code(3 characters) of the origin airport.Example: AMS
        /// destination (optional) : IATA-code (3 characters) of the destination airport.Example: BCN
        /// </param>
        /// <returns></returns>
        public async Task<FlightRoute[]> GetAllRoutesAsync(QueryParams queryParams)
        {
            string queryString = string.Empty;
            if (queryParams != null)
                queryString = queryParams.ToQueryString();
            using HttpResponseMessage response = await _httpClient.GetAsync($"/v3/routes{queryString}");
            if (response.IsSuccessStatusCode)
            {
                FlightRoute[] flightRoutes = await response.Content.ReadAsAsync<FlightRoute[]>();
                return flightRoutes;
            }

            _logger.LogError($"Transavia {response.StatusCode} {await response.Content.ReadAsStringAsync()}");
            return null;
        }
    }
}
