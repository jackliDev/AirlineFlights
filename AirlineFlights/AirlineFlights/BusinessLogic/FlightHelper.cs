using FlightManagement.Enum;
using FlightManagement.Model;
using System.Text.Json;

namespace AirlineFlights.BusinessLogic
{
    public class FlightHelper
    {
        public static List<Order> ReadAndDeserializeJsonFromFile(string dataFilePath)
        {
            string ordersJson;
            try
            {
                ordersJson = File.ReadAllText(dataFilePath);
            }
            catch (Exception e)
            {
                throw new Exception($"Error accessing {dataFilePath}");
            }

            var orders = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, string>>>(ordersJson);
            List<Order> ordersList = new List<Order>();

            foreach (var order in orders)
            {
                bool parseRes = System.Enum.TryParse(order.Value["destination"], out AirportCode parsedDestination);
                var destination = parseRes ? parsedDestination : AirportCode.UNKNOWN;

                var orderObj = new Order
                {
                    Id = order.Key,
                    Destination = destination
                };

                ordersList.Add(orderObj);
            }

            return ordersList;
        }

        public static void printFlights(Dictionary<int, List<Flight>> flightsSchedule)
        {
            foreach (KeyValuePair<int, List<Flight>> flightsForDay in flightsSchedule)
            {
                foreach (Flight flight in flightsForDay.Value)
                {
                    Console.WriteLine($"Flight: {flight.FlightNumber}, departure: {flight.DepartureAirport}, arrival: {flight.ArrivalAirport}, day: {flightsForDay.Key}");
                }
            }
        }

        public static Flight FindFlightWithCapacity(Dictionary<int, List<Flight>> flightsSchedule, AirportCode destination)
        {
            foreach (var day in flightsSchedule.Keys)
            {
                foreach (var flight in flightsSchedule[day])
                {
                    if (flight.ArrivalAirport == destination && flight.CurrentBoxCount < flight.TotalBoxCapacity)
                    {
                        return flight;
                    }
                }
            }

            return null;
        }
    }
}
