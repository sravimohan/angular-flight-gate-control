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
            InitData();
        }

        private void InitData()
        {
            if(_flights != null)
                return;

            _flights = new List<Flight>
            {
                new Flight {Id = new Guid("700225B1-E4E7-4726-A2E0-B09CB1E78808"), Name = "Flight A" },
                new Flight {Id = new Guid("96AD0D4C-8AEB-4122-B4E0-977B452746D9"), Name = "Flight B" },
                new Flight {Id = new Guid("3AED3E4C-EC5F-4E9F-B371-457D6E927602"), Name = "Flight C" },
                new Flight {Id = new Guid("3A532D04-1B9E-4183-8735-1A3DEDFBF1C1"), Name = "Flight D" },
                new Flight {Id = new Guid("B311A950-7646-4786-B0A3-8D43CC9328FF"), Name = "Flight E" },
                new Flight {Id = new Guid("4D6AA116-B751-4788-9245-51CDBD717F83"), Name = "Flight F" },
                new Flight {Id = new Guid("2493F6F0-797D-4212-8505-2AA33B03A453"), Name = "Flight G" },
                new Flight {Id = new Guid("7E7B93E9-F6BC-41D9-B404-D6AFCC653B4B"), Name = "Flight H" },
                new Flight {Id = new Guid("36C3CAEF-5710-4E04-BCFC-11E6EEB4EF69"), Name = "Flight I" },
                new Flight {Id = new Guid("8DC4B515-7A4F-4AAE-BDA5-7DBF2E4B4B85"), Name = "Flight J" }
            };

            _gates = new List<Gate>
            {
                new Gate {Id = new Guid("C8E8FAA6-35AE-4EDC-BA68-42288A05E70E"), Name = "Gate 1"},
                new Gate {Id = new Guid("58DC3F9B-F06D-4694-9E94-66DC38F2F446"), Name = "Gate 2"}
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
                new FlightDetail {Id = new Guid("AAF1638B-1E04-4F73-83B8-56BD21BDBBF2"), ArrivalTime = new DateTime(GetYear,GetMonth,GetDay,10,0,0), DepartureTime = new DateTime(GetYear,GetMonth,GetDay,10,30,0), Flight = flight1, Gate = gate1},
                new FlightDetail {Id = new Guid("5D1B3C1D-5274-4CB0-A121-668F2965E656"), ArrivalTime = new DateTime(GetYear,GetMonth,GetDay,11,0,0), DepartureTime = new DateTime(GetYear,GetMonth,GetDay,11,30,0), Flight = flight2, Gate = gate1},
                new FlightDetail {Id = new Guid("D17DA891-C7B5-4C82-8580-BE90D8304025"), ArrivalTime = new DateTime(GetYear,GetMonth,GetDay,10,0,0), DepartureTime = new DateTime(GetYear,GetMonth,GetDay,10,30,0), Flight = flight3, Gate = gate2},
                new FlightDetail {Id = new Guid("09EC01CC-2A7A-4C82-8D56-7F2D9AC52A7C"), ArrivalTime = new DateTime(GetYear,GetMonth,GetDay,11,0,0), DepartureTime = new DateTime(GetYear,GetMonth,GetDay,11,30,0), Flight = flight4, Gate = gate2},
                new FlightDetail {Id = new Guid("7D39CFF0-8CFE-4839-B906-19555C8F1FC2"), ArrivalTime = new DateTime(GetYear,GetMonth,GetDay,12,0,0), DepartureTime = new DateTime(GetYear,GetMonth,GetDay,12,30,0), Flight = flight5, Gate = gate2},

                new FlightDetail {Id = new Guid("E6312EEB-E82C-45F2-9612-372E43C852CB"), ArrivalTime = new DateTime(GetTomorrowYear,GetTomorrowMonth,GetTomorrowDay,10,0,0), DepartureTime = new DateTime(GetTomorrowYear,GetTomorrowMonth,GetTomorrowDay,10,30,0), Flight = flight1, Gate = gate1},
                new FlightDetail {Id = new Guid("61490A19-30F3-469A-BE90-6C1368591AA8"), ArrivalTime = new DateTime(GetTomorrowYear,GetTomorrowMonth,GetTomorrowDay,11,0,0), DepartureTime = new DateTime(GetTomorrowYear,GetTomorrowMonth,GetTomorrowDay,11,30,0), Flight = flight2, Gate = gate1},
                new FlightDetail {Id = new Guid("922681EA-7C12-4252-83DC-F30EEBB6E460"), ArrivalTime = new DateTime(GetTomorrowYear,GetTomorrowMonth,GetTomorrowDay,10,0,0), DepartureTime = new DateTime(GetTomorrowYear,GetTomorrowMonth,GetTomorrowDay,10,30,0), Flight = flight3, Gate = gate2},
                new FlightDetail {Id = new Guid("F1DB4855-5601-4511-A911-0B59FD621596"), ArrivalTime = new DateTime(GetTomorrowYear,GetTomorrowMonth,GetTomorrowDay,11,0,0), DepartureTime = new DateTime(GetTomorrowYear,GetTomorrowMonth,GetTomorrowDay,11,30,0), Flight = flight4, Gate = gate2},
                new FlightDetail {Id = new Guid("B2594583-D02F-4FE5-BA07-7776D2131022"), ArrivalTime = new DateTime(GetTomorrowYear,GetTomorrowMonth,GetTomorrowDay,12,0,0), DepartureTime = new DateTime(GetTomorrowYear,GetTomorrowMonth,GetTomorrowDay,12,30,0), Flight = flight5, Gate = gate2},
            };
        }

        private int GetYear => DateTime.Now.Year;
        private int GetMonth => DateTime.Now.Month;
        private int GetDay => DateTime.Now.Day;

        private int GetTomorrowYear => DateTime.Now.AddDays(1).Year;
        private int GetTomorrowMonth => DateTime.Now.AddDays(1).Month;
        private int GetTomorrowDay => DateTime.Now.AddDays(1).Day;

    }
}
