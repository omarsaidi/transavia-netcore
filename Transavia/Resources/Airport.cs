using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transavia.Resources
{
    public class Airport
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public Country Country { get; set; }
        public GeoCoordinates GeoCoordinates { get; set; }
        public Self Self { get; set; }
    }
    public class Country
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }
    public class GeoCoordinates
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }

    }
    public class Self
    {
        public string Href { get; set; }
    }
}
