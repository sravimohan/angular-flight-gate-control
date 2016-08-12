using System;

namespace iasset.core.Services
{
    public class FlightScheduleResponse
    {
        public Guid FlightDetailId { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}