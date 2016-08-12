namespace iasset.core.Schedule
{
    public interface IFlightScheduleManager
    {
        bool IsConflict(FlightDetail flightDetail);
        bool AddAndRescheduleOtherFlights(FlightDetail flightDetail);
        Gate FindAlternativeGate(FlightDetail flightDetail);
    }
}