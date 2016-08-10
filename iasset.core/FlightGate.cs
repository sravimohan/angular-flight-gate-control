using System;

namespace iasset.core
{
    public class FlightGate
    {
        public int Id { get; set; }
        public DateTime ArrivalTime { get; set; }
        public DateTime DepartureTime { get; set; }
        public Gate Gate { get; set; }
        public Flight Flight { get; set; }
    }
}
