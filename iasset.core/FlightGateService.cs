using System;
using System.Collections.Generic;
using System.Linq;

namespace iasset.core
{
    public class FlightGateService : IFlightGateService
    {
        private readonly Repository _repository;

        public FlightGateService()
        {
            _repository = new Repository();
        }

        public IEnumerable<FlightGate> GetFlights(int gateId, DateTime date)
        {
            return _repository.FlightGates
                .Where(d => d.Gate.Id.Equals(gateId) && (d.ArrivalTime.Equals(date) || d.DepartureTime.Equals(date)));
        }

        public FlightGate AddorUpdateFlight(int flightId, int gateId, DateTime arrivalDateTime, DateTime departureDateTime)
        {
            var flightGate =_repository.FlightGates.FirstOrDefault(d => d.Flight.Id.Equals(flightId) && d.Gate.Id.Equals(gateId));

            var gate = _repository.Gates.First(g => g.Id.Equals(gateId));
            if(gate == null)
                throw new ArgumentException("Invalid Gate Id");

            var flight = _repository.Flights.First(f => f.Id.Equals(flightId));
            if(flight == null)
                throw new ArgumentException("Invalid Flight Id");

            if (flightGate == null)
            {
                flightGate = new FlightGate();
                _repository.FlightGates.Add(flightGate);
            }

            flightGate.ArrivalTime = arrivalDateTime;
            flightGate.DepartureTime = departureDateTime;
            flightGate.Gate = gate;
            flightGate.Flight = flight;

            return flightGate;
        }

        public void CancelFlight(int flightId, int gateId)
        {
            var flightGate =_repository.FlightGates.FirstOrDefault(d => d.Flight.Id.Equals(flightId) && d.Gate.Id.Equals(gateId));
            if (flightGate == null)
                return;

            _repository.FlightGates.Remove(flightGate);
        }
    }
}
