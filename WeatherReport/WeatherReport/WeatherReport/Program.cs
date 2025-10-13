
using System;
using System.Net.Http;
using System.Runtime.InteropServices.JavaScript;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Enter city names: ");
        string input = Console.ReadLine();
        string[] cities = input.Split(',');
        Task[] tasks = new Task[cities.Length];
        for (int i = 0; i < cities.Length; i++)
        {
            string city = cities[i].Trim();
            tasks[i] = GetWeatherAsync(city);
        }
        await Task.WhenAll(tasks);

    }
    static async Task GetWeatherAsync(string city)
    {
        string apiKey = "6c7e4dbd3bf94b10914113142252909";
        string url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}&units=metric";
        try
        {
            using HttpClient client = new HttpClient();
            string response = await client.GetStringAsync(url);
            JSObject weatherData = JSObject.Parse(response);
            string name = weatherData["name"].ToString();
            string temp = weatherData["main"]["temp"].ToString();
            string description = weatherData["weather"][0]["description"].ToString();
            Console.WriteLine($"City:{name},Temp:{temp}C,Condition:{description}");
        }
        catch (HttpRequestException)
        {
            Console.WriteLine($"Error:Could not fetch weather for{city}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error:{ex.Message}");
        }
    }
}