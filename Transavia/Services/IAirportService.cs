using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transavia.Resources;

namespace Transavia.Services
{
    public interface IAirportService
    {
        Task<Airport[]> GetAirportsByAirportAsync(string id, QueryParams queryParams);
        Task<Airport[]> GetAirportsByCountryCodeAsync(string countryCode);
        Task<Airport[]> GetAirportsByGeoCoordinatesAsync(QueryParams queryParams);
        Task<Airport> GetAirportsByIdAsync(string id);
    }
}
