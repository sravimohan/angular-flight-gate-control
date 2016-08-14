using System;

namespace iasset.core.Services
{
    public static class Guard
    {
        public static void AddFlightDetails(DateTime arrivalDateTime, DateTime departureDateTime)
        {
            if(arrivalDateTime == default(DateTime))
            {
                throw new ArgumentException("Arrival date and time is mandatory");
            }

            if (departureDateTime <= arrivalDateTime)
            {
                throw  new ArgumentException("Departure time must be later than the arrival date");
            }
        }
    }
}
