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
        /// <param name="id">Airport(IATA code) to search nearest airports for.</param>
        /// <param name="queryString">
        /// maxDistanceInKm(optional) : Maximum distance in kilometers, lower than 1 and higher than 500 is not allowed.If not set, max value is applied.
        /// limit(optional) : Limits the result, lower than 0 is not allowed. If not set, the result is not limited.
        /// </param>
        /// <returns>All airports</returns>
        public async Task<Airport[]> GetAirportsByAirportAsync(string id, QueryParams queryParams)
        {
            string queryString = string.Empty;
            if (queryParams != null)
                queryString = queryParams.ToQueryString();
            using HttpResponseMessage response = await _httpClient.GetAsync($"{AirportsEndpointUrl}/nearest/{id}{queryString}");
            if (response.IsSuccessStatusCode)
            {
                Airport[] airports = await response.Content.ReadAsAsync<Airport[]>();
                return airports;
            }

            _logger.LogError($"Transavia {response.StatusCode} {await response.Content.ReadAsStringAsync()}");
            return null;
        }
        /// <summary>
        /// Retrieve airports by country code.
        /// </summary>
        /// <returns>Airports by country code</returns>
        public async Task<Airport[]> GetAirportsByCountryCodeAsync(string countryCode)
        {
            using HttpResponseMessage response = await _httpClient.GetAsync($"{AirportsEndpointUrl}/countrycode/{countryCode}");
            if (response.IsSuccessStatusCode)
            {
                Airport[] airports = await response.Content.ReadAsAsync<Airport[]>();
                return airports;
            }

            _logger.LogError($"Transavia {response.StatusCode} {await response.Content.ReadAsStringAsync()}");
            return null;
        }
        /// <summary>
        /// Retrieve nearest airports by geo coordinates (latitude/longitude).
        /// </summary>
        /// <param name="queryParams">
        /// latitude (optional) : Latitude in decimals, lower than -90.0 and higher than 90.0 is not allowed.
        /// longitude(optional) : Longitude in decimals, lower than -180.0 and higher than 180.0 is not allowed.
        /// maxDistanceInKm(optional) : Maximum distance in kilometers, lower than 1 and higher than 500 is not allowed.If not set, max value is applied.
        /// limit (optional) : Limits the result, lower than 0 is not allowed. If not set, the result is not limited.
        /// </param>
        /// <returns>Nearest airports</returns>
        public async Task<Airport[]> GetAirportsByGeoCoordinatesAsync(QueryParams queryParams)
        {
            string queryString = string.Empty;
            if (queryParams != null)
                queryString = queryParams.ToQueryString();
            using HttpResponseMessage response = await _httpClient.GetAsync($"{AirportsEndpointUrl}/nearest{queryString}");
            if (response.IsSuccessStatusCode)
            {
                Airport[] airports = await response.Content.ReadAsAsync<Airport[]>();
                return airports;
            }

            _logger.LogError($"Transavia {response.StatusCode} {await response.Content.ReadAsStringAsync()}");
            return null;
        }
        /// <summary>
        /// Retrieve airport by id..
        /// </summary>
        /// <param name="id">Airport code (3-character IATA code).</param>
        /// <returns>All airports</returns>
        public async Task<Airport> GetAirportsByIdAsync(string id)
        {
            using HttpResponseMessage response = await _httpClient.GetAsync($"{AirportsEndpointUrl}/{id}");
            if (response.IsSuccessStatusCode)
            {
                Airport airport = await response.Content.ReadAsAsync<Airport>();
                return airport;
            }

            _logger.LogError($"Transavia {response.StatusCode} {await response.Content.ReadAsStringAsync()}");
            return null;
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
