using System;
using System.Collections.Generic;
using System.Linq;
using iasset.core.Repository;

namespace iasset.core.Schedule
{
    public class FlightScheduleManager : IFlightScheduleManager
    {
        private readonly IFlightGateRepository _flightGateRepository;
        private readonly List<FlightDetail> _flightDetails;

        public FlightScheduleManager(IFlightGateRepository flightGateRepository)
        {
            _flightGateRepository = flightGateRepository;
            _flightDetails = _flightGateRepository.FlightDetails.ToList();
        }

        public bool IsConflict(FlightDetail flightDetail)
        {
            var flightsQry = _flightDetails
                .Where(d => d.Gate.Id.Equals(flightDetail.Gate.Id)
                            && (d.ArrivalTime.Year == flightDetail.ArrivalTime.Year
                                && d.ArrivalTime.Month == flightDetail.ArrivalTime.Month
                                && d.ArrivalTime.Day == flightDetail.ArrivalTime.Day))
                .OrderBy(d => d.ArrivalTime)
                .AsQueryable();

            var flightConflictQry = flightsQry.Where(f => (f.ArrivalTime <= flightDetail.DepartureTime) && (f.DepartureTime >= flightDetail.ArrivalTime));
            return flightConflictQry.Any();
        }

        private IEnumerable<FlightDetail> GetConflictingFlightDetails(FlightDetail flightDetail)
        {
            var flightsQry = _flightGateRepository.FlightDetails
                .Where(d => d.Gate.Id.Equals(flightDetail.Gate.Id)
                            && (d.ArrivalTime.Year == flightDetail.ArrivalTime.Year
                                && d.ArrivalTime.Month == flightDetail.ArrivalTime.Month
                                && d.ArrivalTime.Day == flightDetail.ArrivalTime.Day))
                .OrderBy(d => d.ArrivalTime)
                .AsQueryable();

            return flightsQry.Where(f => flightDetail.ArrivalTime >= f.ArrivalTime && f.ArrivalTime <= f.DepartureTime);
        }

        public bool AddAndRescheduleOtherFlights(FlightDetail flightDetail)
        {
            var conflictingFlights = RemoveConflictingFlights(flightDetail);
            AddCurrentFlight(flightDetail);
            ReschduleConflictingFlights(conflictingFlights);
            UpdateRepository();
            return true;
        }

        public Gate FindAlternativeGate(FlightDetail flightDetail)
        {
            foreach (var gate in _flightGateRepository.Gates.Where(g => g.Id != flightDetail.Gate.Id))
            {
                flightDetail.Gate = gate;

                if (!IsConflict(flightDetail))
                    return flightDetail.Gate;
            }

            return null;
        }

        private void UpdateRepository()
        {
            _flightGateRepository.FlightDetails.Clear();
            foreach (var existingFlightDetail in _flightDetails)
            {
                _flightGateRepository.FlightDetails.Add(existingFlightDetail);
            }
        }

        private void ReschduleConflictingFlights(IEnumerable<FlightDetail> conflictingFlights)
        {
            foreach (var conflictingFlight in conflictingFlights)
            {
                var newFlightDetail = FindNextAvailableSlot(conflictingFlight);
                if (newFlightDetail == null)
                    throw new FlightSchedulingException();

                _flightDetails.Add(newFlightDetail);
            }
        }

        private void AddCurrentFlight(FlightDetail flightDetail)
        {
            _flightDetails.Add(flightDetail);
        }

        private IEnumerable<FlightDetail> RemoveConflictingFlights(FlightDetail flightDetail)
        {
            var conflictingFlights = GetConflictingFlightDetails(flightDetail).ToList();
            foreach (var conflictingFlight in conflictingFlights)
            {
                _flightDetails.Remove(conflictingFlight);
            }
            return conflictingFlights;
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