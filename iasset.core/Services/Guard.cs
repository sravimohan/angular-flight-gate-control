using System;

namespace iasset.core.Services
{
    public static class Guard
    {
        public static void AddFlightDetails(DateTime arrivalDateTime, DateTime departureDateTime)
        {
            if (departureDateTime <= arrivalDateTime)
            {
                throw  new ArgumentException("Departure date must be later than the arrival date");
            }
        }
    }
}
