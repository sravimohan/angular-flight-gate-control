using System.Collections.Generic;

namespace iasset.core.Repository
{
    public interface IFlightGateRepository
    {
        void InitData();
        IList<FlightDetail> FlightDetails { get; set; }
        IList<FlightDetail> CloneFlightDetails { get; }

        IList<Flight> Flights { get; }
        IList<Gate> Gates { get; }
    }
}