using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

class Program
{
    static async Task Main(string[] args)
    {
        Console.Write("Enter city names (separated by commas): ");
        string input = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(input))
        {
            Console.WriteLine("No city names entered.");
            return;
        }

        // Split the cities by comma
        string[] cities = input.Split(',');

        // Loop through each city and get weather
        foreach (string cityName in cities)
        {
            string city = cityName.Trim(); // remove extra spaces
            if (city != "")
            {
                await GetWeatherAsync(city);
            }
        }
    }

    static async Task GetWeatherAsync(string city)
    {
        string apiKey = "8a1b302d3dd71a38fd4b0e9b498cda62"; // Your API key
        string url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}&units=metric";

        try
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Could not get weather for {city}. Please check the city name.");
                    return;
                }

                string json = await response.Content.ReadAsStringAsync();
                JObject data = JObject.Parse(json);

                string name = data["name"]?.ToString() ?? "Unknown";
                string temp = data["main"]?["temp"]?.ToString() ?? "N/A";
                string condition = data["weather"]?[0]?["description"]?.ToString() ?? "N/A";

                Console.WriteLine("-------------------------------");
                Console.WriteLine($"City Name   : {name}");
                Console.WriteLine($"Temperature : {temp}°C");
                Console.WriteLine($"Condition   : {condition}");
                Console.WriteLine("-------------------------------");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error while getting data for {city}: {ex.Message}");
        }
    }
}
