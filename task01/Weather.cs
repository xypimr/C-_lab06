namespace task01;
using Newtonsoft.Json;

public class Weather
{   
    [JsonProperty("description")]
    public string Description { get; set; }
}