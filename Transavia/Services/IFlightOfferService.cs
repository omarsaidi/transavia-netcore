using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transavia.Resources;

namespace Transavia.Services
{
    public interface IFlightOfferService
    {
        Task<FlightOffersSerach> GetFlightOffersAsync(QueryParams queryParams);
    }
}
