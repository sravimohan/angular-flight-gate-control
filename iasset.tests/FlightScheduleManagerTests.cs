using System;
using System.Linq;
using iasset.core;
using iasset.core.Repository;
using iasset.core.Schedule;
using iasset.core.Services;
using NUnit.Framework;

namespace iasset.tests
{
    [TestFixture]
    public class FlightScheduleManagerTests
    {
        private readonly IFlightGateService _flightGateService;

        public FlightScheduleManagerTests()
        {
            _flightGateService = new FlightGateService();
        }

        [Test]
        public void AddFlightTest()
        {
            new FlightGateRepository().InitData();
            FlightGateRepository.ClearFlightDetails();

            //setup
            var flight = _flightGateService.GetAllFlights().First();
            var gate = _flightGateService.GetAllGates().First();
            var arrival = new DateTime(2016, 8, 25, 10, 0, 0);
            var departure = new DateTime(2016, 8, 25, 10, 30, 0);
            _flightGateService.AddFlightDetail(flight.Id, gate.Id, arrival, departure);

            var conflictFlightDetail = new FlightDetail
            {
                Id = Guid.NewGuid(),
                ArrivalTime = new DateTime(2016, 8, 25, 10, 15, 0),
                DepartureTime = new DateTime(2016, 8, 25, 10, 45, 0),
                Gate = gate,
                Flight = flight
            };

            //act
            var result = new FlightScheduleManager(new FlightGateRepository()).IsConflict(conflictFlightDetail);

            //assert
            Assert.IsTrue(result);
        }

        [Test]
        public void FildAlternativeGateTest()
        {
            new FlightGateRepository().InitData();
            FlightGateRepository.ClearFlightDetails();

            //setup
            var flight = _flightGateService.GetAllFlights().First();
            var gate = _flightGateService.GetAllGates().First();
            var arrival = new DateTime(2016, 8, 25, 10, 0, 0);
            var departure = new DateTime(2016, 8, 25, 10, 30, 0);
            _flightGateService.AddFlightDetail(flight.Id, gate.Id, arrival, departure);

            var conflictFlightDetail = new FlightDetail
            {
                Id = Guid.NewGuid(),
                ArrivalTime = new DateTime(2016, 8, 25, 10, 15, 0),
                DepartureTime = new DateTime(2016, 8, 25, 10, 45, 0),
                Gate = gate,
                Flight = flight
            };

            //act
            var result = new FlightScheduleManager(new FlightGateRepository())
                .FindAlternativeGate(conflictFlightDetail.Gate, conflictFlightDetail.ArrivalTime, conflictFlightDetail.DepartureTime);

            //assert
            Assert.IsNotNull(result);
            Assert.IsTrue(gate.Id != result.Id);
        }

        [Test]
        public void FildAlternativeSlotTest()
        {
            new FlightGateRepository().InitData();
            FlightGateRepository.ClearFlightDetails();

            //setup
            var flight = _flightGateService.GetAllFlights().First();
            var gate = _flightGateService.GetAllGates().First();
            var arrival = new DateTime(2016, 8, 25, 10, 0, 0);
            var departure = new DateTime(2016, 8, 25, 10, 30, 0);
            _flightGateService.AddFlightDetail(flight.Id, gate.Id, arrival, departure);

            var conflictFlightDetail = new FlightDetail
            {
                Id = Guid.NewGuid(),
                ArrivalTime = new DateTime(2016, 8, 25, 10, 15, 0),
                DepartureTime = new DateTime(2016, 8, 25, 10, 45, 0),
                Gate = gate,
                Flight = flight
            };

            //act
            var result = new FlightScheduleManager(new FlightGateRepository()).AddAndRescheduleOtherFlights(conflictFlightDetail);

            //assert
            Assert.IsTrue(result);
        }
    }
}