using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCClientVSFly.Models
{
    public class Flight
    {
        public int FlightNo { get; set; }

        public string Departure { get; set; }

        public string Destination { get; set; }
        public DateTime Date { get; set; }

        public short? Seats { get; set; }
        public short AvailableSeats { get; set; }

        public double SeatPrice { get; set; }
    }
}
