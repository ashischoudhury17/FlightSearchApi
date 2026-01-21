namespace FlightSearchApi.Data;

public class FlightData
{
    public Dictionary<string, List<string>> Destinations = new()
        {
            { "NYC", new List<string> { "LON", "PAR", "LAX", "BLR" } },
            { "LON", new List<string> { "NYC", "PAR", "BER" } },
            { "DEL", new List<string> { "NYC", "PAR", "BER" } }
        };

    public List<Flight> Flights = new List<Flight>()
        {
            new Flight("DEL", "PAR", DateTime.Today.AddDays(1), 450, "Indigo", ""),
            new Flight("NYC", "PAR", DateTime.Today.AddDays(1), 450, "Air France", ""),
            new Flight("PAR", "NYC", DateTime.Today.AddDays(2), 550, "Air India", ""),
            new Flight("PAR", "DEL", DateTime.Today.AddDays(2), 300, "SWISS", "")
        };
}

public class Flight
{
    public string Origin { get; set; } = "";
    public string Destination { get; set; } = "";
    public DateTime Date { get; set; }
    public decimal Price { get; set; }
    public string Airlines { get; set; } = "";
    public string FlightType { get; set; } = "";

    public Flight(string origin, string destination, DateTime date, decimal price, string airlines, string flightType)
    {
        Origin = origin;
        Destination = destination;
        Date = date;
        Price = price;
        Airlines = airlines;
        FlightType = flightType;
    }
} 
