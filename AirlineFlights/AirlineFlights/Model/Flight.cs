using FlightManagement.Enum;

namespace FlightManagement.Model
{
    public class Flight
    {
        public int FlightNumber { get; set; }
        public AirportCode DepartureAirport { get; set; }
        public AirportCode ArrivalAirport { get; set; }
        public int TotalBoxCapacity { get; set; }

        private int _currentBoxCount;
        public int CurrentBoxCount
        {
            get => _currentBoxCount;
            set
            {
                if (value > TotalBoxCapacity)
                {
                    throw new InvalidOperationException($"Current box count {value} cannot exceed {TotalBoxCapacity}.");
                }
                _currentBoxCount = value;
            }
        }

        public int Day { get; set; }

        public Flight(int flightNumber,
            AirportCode departureAirport,
            AirportCode arrivalAirport,
            int totalBoxCapacity,
            int currentBoxCount,
            int day)
        {
            FlightNumber = flightNumber;
            DepartureAirport = departureAirport;
            ArrivalAirport = arrivalAirport;
            TotalBoxCapacity = totalBoxCapacity;
            CurrentBoxCount = currentBoxCount;
            Day = day;
        }
    }
}