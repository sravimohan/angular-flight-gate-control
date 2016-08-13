using System;
using System.Collections.Generic;
using System.Linq;
using iasset.core.Repository;
using iasset.core.Schedule;

namespace iasset.core.Services
{
    public class FlightGateService : IFlightGateService
    {
        private readonly IFlightGateRepository _flightGateRepository;

        public FlightGateService()
        {
            _flightGateRepository = new FlightGateRepository();
        }

        public IEnumerable<FlightDetail> GetAllFlightDetails()
        {
            return _flightGateRepository.FlightDetails;
        }

        public IEnumerable<FlightDetail> GetFlightDetails(Guid gateId, DateTime date)
        {
            var qry = _flightGateRepository.FlightDetails.AsQueryable()
                .Where(d => d.Gate.Id.Equals(gateId) 
                && ((d.ArrivalTime.Year == date.Year &&  d.ArrivalTime.Month == date.Month && d.ArrivalTime.Day == date.Day) 
                || (d.DepartureTime.Year == date.Year && d.DepartureTime.Month == date.Month && d.DepartureTime.Day == date.Day)))
                .Distinct().OrderBy(f => f.ArrivalTime);

            return qry.AsEnumerable();
        }

        public IEnumerable<Gate> GetAllGates()
        {
            return _flightGateRepository.Gates.OrderBy(g => g.Name);
        }

        public IEnumerable<Flight> GetAllFlights()
        {
            return _flightGateRepository.Flights.OrderBy(f => f.Name);
        }

        public FlightDetail GetFlightDetail(Guid flightDetailId)
        {
            var flightDetail = _flightGateRepository.FlightDetails.FirstOrDefault(d => d.Id.Equals(flightDetailId));
            if (flightDetail == null)
                throw new ArgumentException("Invalid Flight Detail Id");

            return flightDetail;
        }

        public FlightScheduleResponse AddFlightDetail(Guid flightId, Guid gateId, DateTime arrivalDateTime, DateTime departureDateTime)
        {
            if (departureDateTime == default(DateTime))
                departureDateTime = arrivalDateTime.AddMinutes(29);

            Guard.AddFlightDetails(arrivalDateTime, departureDateTime);

            var response = new FlightScheduleResponse();

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

            var scheduleManager = new FlightScheduleManager(_flightGateRepository, false);
            var isConflict = scheduleManager.IsConflict(flightDetail);

            if (isConflict)
            {
                ResolveConflict(response, scheduleManager, flightDetail);
            }
            else
            {
                _flightGateRepository.FlightDetails.Add(flightDetail);
                response.IsSuccess = true;
            }

            response.FlightDetailId = flightDetailId;
            return response;
        }

        private void ResolveConflict(FlightScheduleResponse response, IFlightScheduleManager scheduleManager, FlightDetail flightDetail)
        {
            var newGate = scheduleManager.FindAlternativeGate(flightDetail.Gate, flightDetail.ArrivalTime, flightDetail.DepartureTime);
            if (newGate != null)
            {
                flightDetail.Gate = newGate;
                _flightGateRepository.FlightDetails.Add(flightDetail);

                response.IsSuccess = true;
                response.Message = $"Saved Successfully. But flight secheduled to " + newGate.Name + " due to a scheduling conflict";
                return;
            }

            scheduleManager.AddAndRescheduleOtherFlights(flightDetail);
            response.IsSuccess = true;
            response.Message = $"Saved Successfully. But other flights had to be re-secheduled.";
        }

        public FlightScheduleResponse UpdateFlightDetail(Guid flightDetailId, Guid flightId, Guid gateId, DateTime arrivalDateTime, DateTime departureDateTime)
        {
            if (departureDateTime == default(DateTime))
                departureDateTime = arrivalDateTime.AddMinutes(29);

            var response = new FlightScheduleResponse();
            Guard.AddFlightDetails(arrivalDateTime, departureDateTime);

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

            var scheduleManager = new FlightScheduleManager(_flightGateRepository, true);
            var isConflict = scheduleManager.IsConflict(flightDetail);

            if (isConflict)
            {
                ResolveConflict(response, scheduleManager, flightDetail);
            }

            response.IsSuccess = true;
            response.FlightDetailId = flightDetailId;
            return response;
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
