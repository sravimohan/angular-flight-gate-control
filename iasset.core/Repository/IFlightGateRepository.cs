using System.Collections.Generic;

namespace iasset.core.Repository
{
    public interface IFlightGateRepository
    {
        void InitData();
        IList<FlightDetail> FlightDetails { get; }
        IList<Flight> Flights { get; }
        IList<Gate> Gates { get; }
    }
}