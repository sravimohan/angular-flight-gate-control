using System;
using System.Collections.Generic;

namespace iasset.core.Services
{
    public interface IFlightGateService
    {
        IEnumerable<FlightDetail> GetFlights(Guid gateId, DateTime date);
        IEnumerable<Gate> GetAllGates();
        IEnumerable<Flight> GetAllFlights();
        Guid AddFlightDetail(Guid flightId, Guid gateId, DateTime arrivalDateTime, DateTime departureDateTime);
        void UpdateFlightDetail(Guid flightDetailId, Guid flightId, Guid gateId, DateTime arrivalDateTime, DateTime departureDateTime);
        void CancelFlightDetail(Guid flightDetailId);
    }
}
