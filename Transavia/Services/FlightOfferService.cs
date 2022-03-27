using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Transavia.Extensions;
using Transavia.Resources;

namespace Transavia.Services
{
    public class FlightOfferService : IFlightOfferService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger _logger;
        public FlightOfferService(IHttpClientFactory clientFactory,
            ILogger<FlightOfferService> logger)
        {
            _httpClient = clientFactory.CreateClient("TRANSAVIA_API_HOST");
            _logger = logger;
        }
        /// <summary>
        /// Retrieve flight offers.
        /// </summary>
        /// <param name="queryParams">
        /// origin (optional) : Routekey(single or multiple) of the origin based on airport IATA-code, comma separated
        /// destination (optional) : Routekey(single or multiple) of the destination based on airport IATA-code, comma separated
        /// originDepartureDate (optional) : Date / Range to depart from origin airport.Must be in format yyyyMMdd or yyyyMM.
        /// originArrivalDate (optional) : Date / Range to arrive at origin airport. Must be in format yyyyMMdd or yyyyMM.
        /// destinationDepartureDate (optional) : Date / Range to depart from destination airport. Must be in format yyyyMMdd or yyyyMM.
        /// destinationArrivalDate (optional) : Date / Range to arrive at destination airport. Must be in format yyyyMMdd or yyyyMM.
        /// originDepartureTime (optional) : Time range in which to depart from origin airport. Must be in format (0000-2359).
        /// originArrivalTime(optional) : Time range in which to arrive at origin airport.Must be in format (0000-2359).
        /// destinationDepartureTime (optional) : Time range in which to depart from destination airport.Must be in format (0000-2359).
        /// destinationArrivalTime(optional) : Time range in which to arrive on destination airport.Must be in format (0000-2359).
        /// originDepartureDayOfWeek (optional) : Preferred departureday(s) of week to depart from origin airport, comma separated(mo, th, we).
        /// originArrivalDayOfWeek (optional) : Preferred departureday(s) of week to arrive on origin airport, comma separated(mo, th, we).
        /// destinationDepartureDayOfWeek(optional) : Preferred departureday(s) of week to depart from destination airport, comma separated(mo, th, we).
        /// destinationArrivalDayOfWeek(optional) : Preferred departureday(s) of week to arrive on destination airport, comma separated(mo, th, we).
        /// daysAtDestination(optional) : Duration in days of the time spent on the destination.Return on the same day means 1 day at destination.
        /// directFlight (optional) : When set to true, will only return direct flights.
        /// adults (optional) : Number of adult passengers (default = 1)
        /// children(optional) : Number of children passengers(default = 0)
        /// price(optional) : Price range in euro's or loyalty points for the flight offer.
        /// groupPricing(optional) : Show price-per-adult by default, or price for all passengers when set to true.
        /// productClass(optional) : Product Class or branded fare(basic, plus, max) (default = basic)
        /// offset(optional) : Paging number of the limited result set(default = 0)
        /// include(optional) : Comma-separated list of 'advanced/optional' fields to be included in the response.Allowed values: ClassOfService
        /// orderBy (optional) : Comma-separated list of fields on which the result must be ordered.Allowed values: 'Origin', 'Destination', 'OriginDepartureDate', 'DestinationArrivalDate', 'Price'. For return flights also the values 'DestinationDepartureDate' and 'OriginArrivalDate' can be used. (default = OriginDepartureDate, Origin, Destination)
        /// lowestPricePerDestination(optional) : Return only the lowest price per destination when set to true (default = false)
        /// loyalty(optional) : When set to "FlyingBlue", filtering is based on Flying Blue points and results are returned in Flying Blue points.
        /// limit(optional) : Maximum number of items in the response(default = 100, max = 1000 for one-way / 200 for round-trip). Can also be used in combination with OrderByEnum to return first or last item(default = 1000, max = 1000) If the response is limitted a ResultSet element is added to the response root element containing a link to request the next offset.
        /// </param>
        /// <returns></returns>
        public async Task<FlightOffersSerach> GetFlightOffersAsync(QueryParams queryParams)
        {
            string queryString = string.Empty;
            if (queryParams != null)
                queryString = queryParams.ToQueryString();
            using HttpResponseMessage response = await _httpClient.GetAsync($"/v1/flightoffers{queryString}");
            if (response.IsSuccessStatusCode)
            {
                FlightOffersSerach flightOffers = await response.Content.ReadAsAsync<FlightOffersSerach>();
                return flightOffers;
            }

            _logger.LogError($"Transavia {response.StatusCode} {await response.Content.ReadAsStringAsync()}");
            return null;
        }
    }
}
