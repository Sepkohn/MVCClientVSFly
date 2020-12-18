using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCClientVSFly.Models
{
    public class Passenger
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public int FlightNumber { get; set; }
        public double PaidPrice { get; set; }
    }
}
