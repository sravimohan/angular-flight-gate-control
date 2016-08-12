using System;

namespace iasset.web.Models
{
    public class FlightDetailPut
    {
        public Guid flightId;
        public Guid gateId;
        public DateTime arrivalDateTime;
        public DateTime departureDateTime;
    }
}