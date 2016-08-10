using System;
using System.Linq;
using NUnit.Framework;
using iasset.core;

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
            const int gateId = 1;
            var date = new DateTime(2016, 8, 15, 10, 0, 0);

            //act
            var flights = _flightGateService.GetFlights(gateId, date);

            //assert
            Assert.IsNotNull(flights);
            Assert.AreEqual(2, flights.Count());
        }

        [Test]
        public void AddFlightTest()
        {
            //setup
            const int flightId = 1;
            const int gateId = 2;
            var arrival = new DateTime(2016, 8, 25, 10, 0, 0);
            var departure = new DateTime(2016, 8, 25, 10, 30, 0);

            //act
            var newFlight = _flightGateService.AddorUpdateFlight(flightId, gateId, arrival, departure);

            //assert
            Assert.IsNotNull(newFlight);

            var flights = _flightGateService.GetFlights(gateId, arrival);
            Assert.IsNotNull(flights);
            Assert.AreEqual(1, flights.Count());
        }

        [Test]
        public void UpdateFlightTest()
        {
            //setup
            const int flightId = 1;
            const int gateId = 2;
            var arrival = new DateTime(2016, 8, 26, 10, 0, 0);
            var departure = new DateTime(2016, 8, 26, 10, 30, 0);
            _flightGateService.AddorUpdateFlight(flightId, gateId, arrival, departure);

            //act
            var updatedArrival = new DateTime(2016, 8, 26, 11, 0, 0);
            var updatedDeparture = new DateTime(2016, 8, 26, 10, 30, 0);
            var updatedFlight = _flightGateService.AddorUpdateFlight(flightId, gateId, updatedArrival, updatedDeparture);

            //assert
            Assert.IsNotNull(updatedFlight);

            var flight = _flightGateService.GetFlights(gateId, updatedArrival).First();
            Assert.IsNotNull(flight);
            Assert.AreEqual(updatedArrival, flight.ArrivalTime);
            Assert.AreEqual(updatedDeparture, flight.DepartureTime);
        }
    }
}
