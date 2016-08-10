using System;
using System.Collections.Generic;
using System.Linq;

namespace iasset.core.Repository
{
    public class FlightGateRepository
    {
        private static IList<Flight> _flights;
        private static IList<Gate> _gates;
        private static IList<FlightDetail> _flightDetails;

        public IList<Flight> Flights => _flights;
        public IList<Gate> Gates => _gates;
        public IList<FlightDetail> FlightDetails => _flightDetails;

        public FlightGateRepository()
        {
            _flights = new List<Flight>
            {
                new Flight {Id = Guid.NewGuid(), Name = "Flight A" },
                new Flight {Id = Guid.NewGuid(), Name = "Flight B" },
                new Flight {Id = Guid.NewGuid(), Name = "Flight C" },
                new Flight {Id = Guid.NewGuid(), Name = "Flight D" },
                new Flight {Id = Guid.NewGuid(), Name = "Flight E" },
                new Flight {Id = Guid.NewGuid(), Name = "Flight F" },
                new Flight {Id = Guid.NewGuid(), Name = "Flight G" },
                new Flight {Id = Guid.NewGuid(), Name = "Flight H" },
                new Flight {Id = Guid.NewGuid(), Name = "Flight I" },
                new Flight {Id = Guid.NewGuid(), Name = "Flight J" }
            };

            _gates = new List<Gate>
            {
                new Gate {Id = Guid.NewGuid(), Name = "Gate 1"},
                new Gate {Id = Guid.NewGuid(), Name = "Gate 2"}
            };

            var flight1 = Flights.First(f => f.Name.Equals("Flight A"));
            var flight2 = Flights.First(f => f.Name.Equals("Flight B"));
            var flight3 = Flights.First(f => f.Name.Equals("Flight C"));
            var flight4 = Flights.First(f => f.Name.Equals("Flight D"));
            var flight5 = Flights.First(f => f.Name.Equals("Flight E"));

            var gate1 = Gates.First(g => g.Name.Equals("Gate 1"));
            var gate2 = Gates.First(g => g.Name.Equals("Gate 2"));

            _flightDetails = new List<FlightDetail>
            {
                new FlightDetail {Id = Guid.NewGuid(), ArrivalTime = new DateTime(2016,8,15,10,0,0), DepartureTime = new DateTime(2016,8,15,10,30,0), Flight = flight1, Gate = gate1},
                new FlightDetail {Id = Guid.NewGuid(), ArrivalTime = new DateTime(2016,8,15,10,0,0), DepartureTime = new DateTime(2016,8,15,10,30,0), Flight = flight2, Gate = gate1},
                new FlightDetail {Id = Guid.NewGuid(), ArrivalTime = new DateTime(2016,8,15,10,0,0), DepartureTime = new DateTime(2016,8,15,10,30,0), Flight = flight3, Gate = gate2},
                new FlightDetail {Id = Guid.NewGuid(), ArrivalTime = new DateTime(2016,8,15,10,0,0), DepartureTime = new DateTime(2016,8,15,10,30,0), Flight = flight4, Gate = gate2},
                new FlightDetail {Id = Guid.NewGuid(), ArrivalTime = new DateTime(2016,8,15,10,0,0), DepartureTime = new DateTime(2016,8,15,10,30,0), Flight = flight5, Gate = gate2},

                new FlightDetail {Id = Guid.NewGuid(), ArrivalTime = new DateTime(2016,8,16,10,0,0), DepartureTime = new DateTime(2016,8,16,10,30,0), Flight = flight1, Gate = gate1},
                new FlightDetail {Id = Guid.NewGuid(), ArrivalTime = new DateTime(2016,8,16,10,0,0), DepartureTime = new DateTime(2016,8,16,10,30,0), Flight = flight2, Gate = gate1},
                new FlightDetail {Id = Guid.NewGuid(), ArrivalTime = new DateTime(2016,8,16,10,0,0), DepartureTime = new DateTime(2016,8,16,10,30,0), Flight = flight3, Gate = gate2},
                new FlightDetail {Id = Guid.NewGuid(), ArrivalTime = new DateTime(2016,8,16,10,0,0), DepartureTime = new DateTime(2016,8,16,10,30,0), Flight = flight4, Gate = gate2},
                new FlightDetail {Id = Guid.NewGuid(), ArrivalTime = new DateTime(2016,8,16,10,0,0), DepartureTime = new DateTime(2016,8,16,10,30,0), Flight = flight5, Gate = gate2},
            };
        }
    }
}
