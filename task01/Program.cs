using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Threading.Channels;
using Newtonsoft.Json;

namespace task01;

class Program
{
    static HttpClient client = new();

    static void Main()
    {
        RunAsync().GetAwaiter().GetResult();
    }
    static async Task RunAsync()
    {
        Random rnd = new Random();
        var API_KEY = "9f74a9ce6cc82482fabb284c2894a2f2";
        var UPPER = 50;
        List<WeatherOBJ> Collection = new List<WeatherOBJ>();
        
        
        for (int i = 0; i < UPPER; i++)
        {
            double lon = rnd.Next(-180,180);
            double lat = rnd.Next(-90, 90);
            string url = $"http://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&appid={API_KEY}";
            var response = await client.GetAsync(url);
            var text_res = await response.Content.ReadAsStringAsync();
            WeatherOBJ wl = JsonConvert.DeserializeObject<WeatherOBJ>(text_res);
            if (wl.Country == "" || wl.Name == "")
                UPPER++;
            else
            {
                Collection.Add(wl);
            }
        }

        // foreach (var VARIABLE in Collection)
        // {
        //     Console.WriteLine($"{VARIABLE.Country}; {VARIABLE.Name}; {VARIABLE.Description}; {VARIABLE.Temp}");
        // }
        //
        // Console.WriteLine('\n');
        // Температура
    
        double MinT = (from w in Collection select w.Temp).Min();
        var countryMinT = from w in Collection where w.Temp == MinT select w.Country;
        foreach (var VARIABLE in countryMinT)
        {
            Console.WriteLine($"min temp: {VARIABLE} - {MinT}C");
        }
        double MaxT = (from w in Collection select w.Temp).Max();
        var countryMaxT = from w in Collection where w.Temp == MaxT select w.Country;
        foreach (var VARIABLE in countryMaxT)
        {
            Console.WriteLine($"max temp: {VARIABLE} - {MaxT}C");
        }
        
        // Количество стран
        
        int Count = (from w in Collection select w.Country).Distinct().Count();
        Console.WriteLine($"Count of countries: {Count}");
        
        // Средняя температура

        double Ave = (from w in Collection select w.Temp).Average();
        Console.WriteLine($"Average temp: {Ave}C");
        
        // clear sky & rain & few clouds
        
        string[] arr = new[] {"clear sky", "rain", "few clouds" };
        foreach (var VARIABLE in arr)
        {
            try
            {
                var weatherLoc = (from w in Collection where w.Description == VARIABLE select w).First();
                Console.WriteLine($"{VARIABLE}: {weatherLoc.Country}; {weatherLoc.Name}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"No places with {VARIABLE}");
            }
                
                
        }
    }
}
