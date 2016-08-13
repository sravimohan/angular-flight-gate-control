using System;
using System.Collections.Generic;

namespace iasset.core.Services
{
    public interface IFlightGateService
    {
        IEnumerable<FlightDetail> GetAllFlightDetails();
        IEnumerable<FlightDetail> GetFlightDetails(Guid gateId, DateTime date);
        IEnumerable<Gate> GetAllGates();
        IEnumerable<Flight> GetAllFlights();

        FlightDetail GetFlightDetail(Guid flightDetailId);
        FlightScheduleResponse AddFlightDetail(Guid flightId, Guid gateId, DateTime arrivalDateTime, DateTime departureDateTime);
        FlightScheduleResponse UpdateFlightDetail(Guid flightDetailId, Guid flightId, Guid gateId, DateTime arrivalDateTime, DateTime departureDateTime);
        void CancelFlightDetail(Guid flightDetailId);
    }
}
