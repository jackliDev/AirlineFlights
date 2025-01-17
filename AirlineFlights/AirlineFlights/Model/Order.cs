using FlightManagement.Enum;

namespace FlightManagement.Model
{
    public class Order
    {
        public string Id { get; set; }
        public AirportCode Destination { get; set; }
    }
}