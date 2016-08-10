using System;
using System.Collections.Generic;
using System.Linq;

namespace iasset.core
{
    public class Repository
    {
        private static IList<Flight> _flights;
        private static IList<Gate> _gates;
        private static IList<FlightGate> _flightGates;

        public IList<Flight> Flights => _flights;
        public IList<Gate> Gates => _gates;
        public IList<FlightGate> FlightGates => _flightGates;

        public Repository()
        {
            _flights = new List<Flight>
            {
                new Flight {Id = 1, Name = "Flight A" },
                new Flight {Id = 2, Name = "Flight B" },
                new Flight {Id = 3, Name = "Flight C" },
                new Flight {Id = 4, Name = "Flight D" },
                new Flight {Id = 5, Name = "Flight E" },
                new Flight {Id = 6, Name = "Flight F" },
                new Flight {Id = 7, Name = "Flight G" },
                new Flight {Id = 8, Name = "Flight H" },
                new Flight {Id = 9, Name = "Flight I" },
                new Flight {Id = 10, Name = "Flight J" }
            };

            _gates = new List<Gate>
            {
                new Gate {Id =1, Name = "Gate 1"},
                new Gate {Id =2, Name = "Gate 2"}
            };

            var flight1 = Flights.First(f => f.Id.Equals(1));
            var flight2 = Flights.First(f => f.Id.Equals(2));
            var flight3 = Flights.First(f => f.Id.Equals(3));
            var flight4 = Flights.First(f => f.Id.Equals(4));
            var flight5 = Flights.First(f => f.Id.Equals(5));

            var gate1 = Gates.First(g => g.Id.Equals(1));
            var gate2 = Gates.First(g => g.Id.Equals(2));

            _flightGates = new List<FlightGate>
            {
                new FlightGate {Id = 1, ArrivalTime = new DateTime(2016,8,15,10,0,0), DepartureTime = new DateTime(2016,8,15,10,30,0), Flight = flight1, Gate = gate1},
                new FlightGate {Id = 2, ArrivalTime = new DateTime(2016,8,15,10,0,0), DepartureTime = new DateTime(2016,8,15,10,30,0), Flight = flight2, Gate = gate1},
                new FlightGate {Id = 3, ArrivalTime = new DateTime(2016,8,15,10,0,0), DepartureTime = new DateTime(2016,8,15,10,30,0), Flight = flight3, Gate = gate2},
                new FlightGate {Id = 4, ArrivalTime = new DateTime(2016,8,15,10,0,0), DepartureTime = new DateTime(2016,8,15,10,30,0), Flight = flight4, Gate = gate2},
                new FlightGate {Id = 5, ArrivalTime = new DateTime(2016,8,15,10,0,0), DepartureTime = new DateTime(2016,8,15,10,30,0), Flight = flight5, Gate = gate2},

                new FlightGate {Id = 6, ArrivalTime = new DateTime(2016,8,16,10,0,0), DepartureTime = new DateTime(2016,8,16,10,30,0), Flight = flight1, Gate = gate1},
                new FlightGate {Id = 7, ArrivalTime = new DateTime(2016,8,16,10,0,0), DepartureTime = new DateTime(2016,8,16,10,30,0), Flight = flight2, Gate = gate1},
                new FlightGate {Id = 8, ArrivalTime = new DateTime(2016,8,16,10,0,0), DepartureTime = new DateTime(2016,8,16,10,30,0), Flight = flight3, Gate = gate2},
                new FlightGate {Id = 9, ArrivalTime = new DateTime(2016,8,16,10,0,0), DepartureTime = new DateTime(2016,8,16,10,30,0), Flight = flight4, Gate = gate2},
                new FlightGate {Id = 10, ArrivalTime = new DateTime(2016,8,16,10,0,0), DepartureTime = new DateTime(2016,8,16,10,30,0), Flight = flight5, Gate = gate2},
            };
        }
    }
}
