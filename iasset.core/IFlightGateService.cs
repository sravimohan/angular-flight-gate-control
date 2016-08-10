using System;
using System.Collections.Generic;

namespace iasset.core
{
    public interface IFlightGateService
    {
        IEnumerable<FlightGate> GetFlights(int gateId, DateTime date);

        FlightGate AddorUpdateFlight(int flightId, int gateId, DateTime arrivalDateTime, DateTime departureDateTime);

        void CancelFlight(int flightId, int gateId);
    }
}
