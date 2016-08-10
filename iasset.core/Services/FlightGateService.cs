using System;
using System.Collections.Generic;
using System.Linq;

namespace iasset.core.Services
{
    public class FlightGateService : IFlightGateService
    {
        private readonly Repository.FlightGateRepository _flightGateRepository;

        public FlightGateService()
        {
            _flightGateRepository = new Repository.FlightGateRepository();
        }

        public IEnumerable<FlightDetail> GetAllFlightDetails()
        {
            return _flightGateRepository.FlightDetails;
        }

        public IEnumerable<FlightDetail> GetFlightDetails(Guid gateId, DateTime date)
        {
            var qry = _flightGateRepository.FlightDetails.AsQueryable()
                .Where(d => d.Gate.Id.Equals(gateId) 
                && ((d.ArrivalTime.Year >= date.Year &&  d.ArrivalTime.Month == date.Month && d.ArrivalTime.Day == date.Day) 
                || (d.DepartureTime.Year >= date.Year && d.DepartureTime.Month == date.Month && d.DepartureTime.Day == date.Day))).Distinct();

            return qry.AsEnumerable();
        }

        public IEnumerable<Gate> GetAllGates()
        {
            return _flightGateRepository.Gates;
        }

        public IEnumerable<Flight> GetAllFlights()
        {
            return _flightGateRepository.Flights;
        }

        public FlightDetail GetFlightDetail(Guid flightDetailId)
        {
            var flightDetail = _flightGateRepository.FlightDetails.FirstOrDefault(d => d.Id.Equals(flightDetailId));
            if (flightDetail == null)
                throw new ArgumentException("Invalid Flight Detail Id");

            return flightDetail;
        }

        public Guid AddFlightDetail(Guid flightId, Guid gateId, DateTime arrivalDateTime, DateTime departureDateTime)
        {
            var gate = _flightGateRepository.Gates.First(g => g.Id.Equals(gateId));
            if (gate == null)
                throw new ArgumentException("Invalid Gate Id");

            var flight = _flightGateRepository.Flights.First(f => f.Id.Equals(flightId));
            if (flight == null)
                throw new ArgumentException("Invalid Flight Id");

            var flightDetailId = Guid.NewGuid();
            var flightDetail = new FlightDetail
            {
                Id = flightDetailId,
                ArrivalTime = arrivalDateTime,
                DepartureTime = departureDateTime,
                Gate = gate,
                Flight = flight
            };

            _flightGateRepository.FlightDetails.Add(flightDetail);
            return flightDetailId;
        }

        public void UpdateFlightDetail(Guid flightDetailId, Guid flightId, Guid gateId, DateTime arrivalDateTime, DateTime departureDateTime)
        {
            var flightDetail = _flightGateRepository.FlightDetails.FirstOrDefault(d => d.Id.Equals(flightDetailId));
            if (flightDetail == null)
                throw new ArgumentException("Invalid Flight Detail Id");

            var gate = _flightGateRepository.Gates.First(g => g.Id.Equals(gateId));
            if (gate == null)
                throw new ArgumentException("Invalid Gate Id");

            var flight = _flightGateRepository.Flights.First(f => f.Id.Equals(flightId));
            if (flight == null)
                throw new ArgumentException("Invalid Flight Id");

            flightDetail.ArrivalTime = arrivalDateTime;
            flightDetail.DepartureTime = departureDateTime;
            flightDetail.Gate = gate;
            flightDetail.Flight = flight;
        }

        public void CancelFlightDetail(Guid flightDetailId)
        {
            var flightDetail =_flightGateRepository.FlightDetails.FirstOrDefault(d => d.Id.Equals(flightDetailId));
            if (flightDetail == null)
                return;

            _flightGateRepository.FlightDetails.Remove(flightDetail);
        }
    }
}
