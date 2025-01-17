using AirlineFlights.BusinessLogic;
using FlightManagement.Enum;
using FlightManagement.Model;

namespace FlightManagement
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // key = day #, value = list of flights for given day
            Dictionary<int, List<Flight>> flightsSchedule = new Dictionary<int, List<Flight>>();
            List<Flight> day1_Flights = new List<Flight>();
            day1_Flights.Add(new Flight(1, AirportCode.YUL, AirportCode.YYZ, 20, 0, 1));
            day1_Flights.Add(new Flight(2, AirportCode.YUL, AirportCode.YYC, 20, 0, 1));
            day1_Flights.Add(new Flight(3, AirportCode.YUL, AirportCode.YVR, 20, 0, 1));
            flightsSchedule.Add(1, day1_Flights);

            List<Flight> day2_Flights = new List<Flight>();
            day2_Flights.Add(new Flight(4, AirportCode.YUL, AirportCode.YYZ, 20, 0, 2));
            day2_Flights.Add(new Flight(5, AirportCode.YUL, AirportCode.YYC, 20, 0, 2));
            day2_Flights.Add(new Flight(6, AirportCode.YUL, AirportCode.YVR, 20, 0, 2));
            flightsSchedule.Add(2, day2_Flights);

            Console.WriteLine("User story #1 - Print Flight Schedule");
            FlightHelper.printFlights(flightsSchedule);

            string dataFilePath = @"./Data/orders.json";
            List<Order> ordersList = FlightHelper.ReadAndDeserializeJsonFromFile(dataFilePath);
            Console.WriteLine();

            Console.WriteLine("User story #2 - Print Orders and Flight Info");
            foreach (var order in ordersList)
            {
                var flight = FlightHelper.FindFlightWithCapacity(flightsSchedule, order.Destination);

                if (flight == null)
                {
                    Console.WriteLine($"order: {order.Id}, flightNumber: not scheduled");
                }
                else
                {
                    Console.WriteLine($"order: {order.Id}, flightNumber: {flight.FlightNumber}, departure: {flight.DepartureAirport}, arrival: {flight.ArrivalAirport}, day: {flight.Day}");
                    flight.CurrentBoxCount = flight.CurrentBoxCount + 1;
                }
            }

            Console.ReadKey();
        }
    }
}
