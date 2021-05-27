using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Transavia.Resources
{
    public class FlightOffersSerach
    {
        public ResultSet ResultSet { get; set; }
        [JsonPropertyName("flightOffer")]
        public FlightOffer[] FlightOffers { get; set; }
    }
    public class ResultSet
    {
        public int Count { get; set; }
    }
    public class FlightOffer
    {

        public OutboundFlight OutboundFlight { get; set; }
        public PricingInfoSum PricingInfoSum { get; set; }
        public Deeplink Deeplink { get; set; }
    }
    public class OutboundFlight
    {
        public string Id { get; set; }
        public DateTime DepartureDateTime { get; set; }
        public DateTime AarrivalDateTime { get; set; }
        public MarketingAirline MarketingAirline { get; set; }
        public int FlightNumber { get; set; }
        public Airport DepartureAirport { get; set; }
        public Airport ArrivalAirport { get; set; }
        public class Airport
        {
            public string LocationCode { get; set; }
        }
    }
    public class MarketingAirline
    {
        public string CompanyShortName { get; set; }
    }
    public class PricingInfoSum
    {
        public double TotalPriceAllPassengers { get; set; }
        public double TotalPriceOnePassenger { get; set; }
        public double BaseFare { get; set; }
        public double TaxSurcharge { get; set; }
        public string CurrencyCode { get; set; }
        public string ProductClass { get; set; }
    }
    public class Deeplink
    {
        public string Href { get; set; }
    }

}
