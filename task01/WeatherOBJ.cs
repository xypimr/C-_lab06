using Newtonsoft.Json;

namespace task01;

public struct WeatherOBJ
{   
    [JsonProperty("weather")]
    public Weather[] Weather { get; set; }
    public string Description => Weather[0].Description;
    [JsonProperty("main")]
    public Main Main { get; set; }
    public double Temp => Main.Temp;

    [JsonProperty("sys")]
    public Sys Sys { get; set; }
    public string Country => Sys.Country;

    [JsonProperty("name")] 
    public string Name { get; set; }


}