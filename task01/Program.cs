using Newtonsoft.Json;

namespace task01;

class Program
{
    static HttpClient _client = new();

    static void Main()
    {
        RunAsync().GetAwaiter().GetResult();
    }
    static async Task RunAsync()
    {
        Random rnd = new Random();
        var apiKey = "9f74a9ce6cc82482fabb284c2894a2f2";
        var upper = 50;
        List<WeatherOBJ> collection = new List<WeatherOBJ>();
        
        
        for (int i = 0; i < upper; i++)
        {
            double lon = rnd.Next(-180,180);
            double lat = rnd.Next(-90, 90);
            string url = $"http://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&appid={apiKey}";
            var response = await _client.GetAsync(url);
            var textRes = await response.Content.ReadAsStringAsync();
            WeatherOBJ wl = JsonConvert.DeserializeObject<WeatherOBJ>(textRes);
            if (wl.Country == "" || wl.Name == "")
                upper++;
            else
                collection.Add(wl);
        }

        foreach (var variable in collection)
        {
            Console.WriteLine($"{variable.Country}; {variable.Name}; {variable.Description}; {variable.Temp}");
        }
        
        Console.WriteLine('\n');
        
        // Температура
    
        double minT = (from w in collection select w.Temp).Min();
        var countryMinT = from w in collection where w.Temp == minT select w.Country;
        foreach (var variable in countryMinT)
        {
            Console.WriteLine($"min temp: {variable} - {minT}C");
        }
        double maxT = (from w in collection select w.Temp).Max();
        var countryMaxT = from w in collection where w.Temp == maxT select w.Country;
        foreach (var variable in countryMaxT)
        {
            Console.WriteLine($"max temp: {variable} - {maxT}C");
        }
        
        // Количество стран
        
        int count = (from w in collection select w.Country).Distinct().Count();
        Console.WriteLine($"Count of countries: {count}");
        
        // Средняя температура

        double ave = (from w in collection select w.Temp).Average();
        Console.WriteLine($"Average temp: {ave}C");
        
        // clear sky & rain & few clouds
        
        string[] arr = new[] {"clear sky", "rain", "few clouds" };
        foreach (var variable in arr)
        {
            try
            {
                var weatherLoc = (from w in collection where w.Description == variable select w).First();
                Console.WriteLine($"{variable}: {weatherLoc.Country}; {weatherLoc.Name}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"No places with {variable}");
            }
                
                
        }
    }
}
