using System;
using System.Linq;
using iasset.core.Repository;
using NUnit.Framework;
using iasset.core.Services;

namespace iasset.tests
{
    [TestFixture]
    public class FlightGateServiceTests
    {
        private readonly IFlightGateService _flightGateService;

        public FlightGateServiceTests()
        {
            _flightGateService = new FlightGateService();
        }

        [Test]
        public void GetFlightsTest()
        {
            //setup
            new FlightGateRepository().InitData(true);
            var gate = _flightGateService.GetAllGates().First();
            var date = DateTime.Now;

            //act
            var flights = _flightGateService.GetFlightDetails(gate.Id, date);

            //assert
            Assert.IsNotNull(flights);
            Assert.AreEqual(5, flights.Count());
        }

        [Test]
        public void AddFlightTest()
        {
            //setup
            var flight = _flightGateService.GetAllFlights().First();
            var gate = _flightGateService.GetAllGates().First();
            var arrival = new DateTime(2016, 8, 25, 10, 0, 0);
            var departure = new DateTime(2016, 8, 25, 10, 30, 0);

            //act
            _flightGateService.AddFlightDetail(flight.Id, gate.Id, arrival, departure);

            //assert
            var flights = _flightGateService.GetFlightDetails(gate.Id, arrival);
            Assert.IsNotNull(flights);
            Assert.AreEqual(1, flights.Count());
        }

        [Test]
        public void UpdateFlightTest()
        {
            //setup
            new FlightGateRepository().InitData(true);
            var flight = _flightGateService.GetAllFlights().First();
            var gate = _flightGateService.GetAllGates().First();
            var arrival = new DateTime(2016, 8, 25, 10, 0, 0);
            var departure = new DateTime(2016, 8, 25, 10, 30, 0);
            var response = _flightGateService.AddFlightDetail(flight.Id, gate.Id, arrival, departure);

            //act
            var updatedArrival = new DateTime(2016, 8, 26, 11, 0, 0);
            var updatedDeparture = new DateTime(2016, 8, 26, 11, 30, 0);
            _flightGateService.UpdateFlightDetail(response.FlightDetailId, flight.Id, gate.Id, updatedArrival, updatedDeparture);

            //assert
            var updatedFlight = _flightGateService.GetFlightDetails(gate.Id, updatedArrival).First();
            Assert.IsNotNull(updatedFlight);
            Assert.AreEqual(updatedArrival, updatedFlight.ArrivalTime);
            Assert.AreEqual(updatedDeparture, updatedFlight.DepartureTime);
        }

        [Test]
        public void CancelFlightTest()
        {
            //setup
            var flight = _flightGateService.GetAllFlights().First();
            var gate = _flightGateService.GetAllGates().First();
            var arrival = new DateTime(2016, 8, 28, 10, 0, 0);
            var departure = new DateTime(2016, 8, 28, 10, 30, 0);
            var response = _flightGateService.AddFlightDetail(flight.Id, gate.Id, arrival, departure);

            //pre-assert
            var preflights = _flightGateService.GetFlightDetails(gate.Id, arrival);
            Assert.IsNotNull(preflights);
            Assert.AreEqual(1, preflights.Count());

            //act
            _flightGateService.CancelFlightDetail(response.FlightDetailId);

            //assert
            var flights = _flightGateService.GetFlightDetails(gate.Id, arrival);
            Assert.IsNotNull(flights);
            Assert.AreEqual(0, flights.Count());
        }
    }
}
