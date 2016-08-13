using System;
using System.Collections.Generic;
using System.Linq;
using iasset.core.Repository;

namespace iasset.core.Schedule
{
    public class FlightScheduleManager : IFlightScheduleManager
    {
        private readonly bool _isUpdate;
        private readonly IFlightGateRepository _flightGateRepository;
        private readonly List<FlightDetail> _flightDetails;

        public FlightScheduleManager(IFlightGateRepository flightGateRepository, bool isUpdate)
        {
            _flightGateRepository = flightGateRepository;
            _isUpdate = isUpdate;
            _flightDetails = _flightGateRepository.CloneFlightDetails.ToList();
        }

        public bool IsConflict(FlightDetail flightDetail)
        {
            var flightsQry = _flightDetails
                .Where(d => d.Gate.Id.Equals(flightDetail.Gate.Id))
                .OrderBy(d => d.ArrivalTime)
                .AsQueryable();

            flightsQry = flightsQry.Where(f => (f.ArrivalTime <= flightDetail.DepartureTime) && (f.DepartureTime >= flightDetail.ArrivalTime));

            if(_isUpdate)
            {
                flightsQry = flightsQry.Where(f => f.Id != flightDetail.Id);
            }

            return flightsQry.Any();
        }

        private IEnumerable<FlightDetail> GetConflictingFlights(FlightDetail flightDetail)
        {
            var flightsQry = _flightDetails
                .Where(d => d.Gate.Id.Equals(flightDetail.Gate.Id))
                .OrderBy(d => d.ArrivalTime)
                .AsQueryable();

            if (_isUpdate)
            {
                flightsQry = flightsQry.Where(f => f.Id != flightDetail.Id);
            }

            return flightsQry.Where(f => (f.ArrivalTime <= flightDetail.DepartureTime) && (f.DepartureTime >= flightDetail.ArrivalTime));
        }

        private IEnumerable<FlightDetail> GetFollowingFlightDetails(FlightDetail flightDetail)
        {
            var flightsQry = _flightDetails.Where(d => d.Gate.Id.Equals(flightDetail.Gate.Id)
                            && (d.ArrivalTime.Year == flightDetail.ArrivalTime.Year
                                && d.ArrivalTime.Month == flightDetail.ArrivalTime.Month
                                && d.ArrivalTime.Day == flightDetail.ArrivalTime.Day))
                .OrderBy(d => d.ArrivalTime)
                .AsQueryable();

            if (_isUpdate)
            {
                flightsQry = flightsQry.Where(f => f.Id != flightDetail.Id);
            }

            return flightsQry.Where(f => f.ArrivalTime >= flightDetail.ArrivalTime);
        }

        public bool AddAndRescheduleOtherFlights(FlightDetail flightDetail)
        {
            var conflictingFlights = RemoveConflictingFlights(flightDetail);

            if (!_isUpdate)
            {
                AddCurrentFlight(flightDetail);
            }
            
            RescheduleConflictingFlights(conflictingFlights);

            UpdateRepository();
            return true;
        }

        public Gate FindAlternativeGate(Gate currentGate, DateTime arrivalTime, DateTime departureTime)
        {
            var flightDetail = new FlightDetail
            {
                ArrivalTime = arrivalTime,
                DepartureTime = departureTime,
                Gate = currentGate
            };

            foreach (var gate in _flightGateRepository.Gates.Where(g => g.Id != currentGate.Id))
            {
                flightDetail.Gate = gate;

                if (!IsConflict(flightDetail))
                    return flightDetail.Gate;
            }

            return null;
        }

        private void UpdateRepository()
        {
            _flightGateRepository.FlightDetails = _flightDetails;
        }

        private void RescheduleConflictingFlights(IEnumerable<FlightDetail> conflictingFlights)
        {
            foreach (var conflictingFlight in conflictingFlights)
            {
                var newFlightDetail = FindNextAvailableSlot(conflictingFlight);
                if (newFlightDetail == null)
                {
                    throw new FlightSchedulingException();
                }

                _flightDetails.Add(newFlightDetail);
            }
        }

        private void AddCurrentFlight(FlightDetail flightDetail)
        {
            _flightDetails.Add(flightDetail);
        }

        private IEnumerable<FlightDetail> RemoveConflictingFlights(FlightDetail flightDetail)
        {
            var conflictingFlights = GetConflictingFlights(flightDetail);
            var followingFlights = GetFollowingFlightDetails(flightDetail);
            var removedFlights = conflictingFlights.Union(followingFlights).Distinct().ToList();

            foreach (var flight in removedFlights)
            {
                _flightDetails.Remove(flight);
            }

            return removedFlights;
        }

        private FlightDetail FindNextAvailableSlot(FlightDetail flightDetail)
        {
            while (true)
            {
                if (!IsConflict(flightDetail))
                    break;

                if (flightDetail.ArrivalTime.Date != flightDetail.ArrivalTime.AddMinutes(5).Date)
                    return null;

                flightDetail.ArrivalTime = flightDetail.ArrivalTime.AddMinutes(5);
                flightDetail.DepartureTime = flightDetail.DepartureTime.AddMinutes(5);
            }

            return flightDetail;
        }
    }
}