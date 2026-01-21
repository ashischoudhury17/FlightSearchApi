using Microsoft.AspNetCore.Mvc;
using FlightSearchApi.Data;

[ApiController]
[Route("api/[controller]")]
public class FlightsController : ControllerBase
{
    private readonly ILogger<FlightsController> _logger;
    public FlightsController(ILogger<FlightsController> logger) => _logger = logger;

    [HttpGet("origins")]
    public IActionResult GetOrigins()
    {
        _logger.LogInformation("Request at {time}: ", DateTime.UtcNow);

        FlightData flightData = new FlightData();

        return Ok(flightData.Destinations.Keys);
    }

    [HttpGet("destinations/{origin}")]
    public IActionResult GetDestinations(string origin)
    {
        _logger.LogInformation("Request at {time}: Origin={origin}", DateTime.UtcNow, origin);

        if (string.IsNullOrWhiteSpace(origin))
            return BadRequest("Origin cannot be empty");

        FlightData flightData = new FlightData();

        if (!flightData.Destinations.ContainsKey(origin))
            return NotFound($"No destinations found for origin {origin}");

        return Ok(flightData.Destinations[origin]);
    }

    [HttpGet("search")]
    public IActionResult SearchFlights([FromQuery] string origin, [FromQuery] string destination,
                                       [FromQuery] DateTime departureDate, [FromQuery] DateTime? returnDate = null)
    {
        _logger.LogInformation("Search request at {time}: Origin={origin}, Destination={destination}", DateTime.UtcNow, origin, destination);

        if (string.IsNullOrWhiteSpace(origin) || string.IsNullOrWhiteSpace(destination))
            return BadRequest("Origin and Destination are required");

        FlightData flightData = new FlightData();
        List<Flight> flights = flightData.Flights
            .Where(f => f.Origin == origin && f.Destination == destination && f.Date.Date == departureDate.Date)
            .ToList();
        foreach (var flight in flights)
        {
            flight.FlightType = "One Way";
        }
        if (returnDate.HasValue)
        {
            var returnFlights = flightData.Flights
                .Where(f => f.Origin == destination && f.Destination == origin && f.Date.Date == returnDate.Value.Date)
                .ToList();
            foreach (var flight in returnFlights) { 
                flight.FlightType = "Return Flight"; 
            }
            flights.AddRange(returnFlights);
        }

        if (!flights.Any())
            return NotFound("No flights found");

        return Ok(flights);
    }
}
