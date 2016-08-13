using System.Collections.Generic;

namespace iasset.core.Repository
{
    public interface IFlightGateRepository
    {
        IList<FlightDetail> FlightDetails { get; set; }
        IList<FlightDetail> CloneFlightDetails { get; }

        IList<Flight> Flights { get; }
        IList<Gate> Gates { get; }
    }
}