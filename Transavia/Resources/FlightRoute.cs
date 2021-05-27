using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transavia.Resources
{
    public class FlightRoute
    {
        public string Id { get; set; }
        public Airport Origin { get; set; }
        public Airport Destination { get; set; }
        public Self Self { get; set; }

        public class Airport
        {
            public string Id { get; set; }
            public Self Self { get; set; }

        }
    }
}
