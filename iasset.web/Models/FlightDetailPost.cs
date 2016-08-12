using System;

namespace iasset.web.Models
{
    public class FlightDetailPost
    {
        public Guid flightDetailId;
        public Guid flightId;
        public Guid gateId;
        public DateTime arrivalDateTime;
        public DateTime departureDateTime;
    }
}