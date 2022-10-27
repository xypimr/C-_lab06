using Newtonsoft.Json;

namespace task01;

public class Sys
{
    [JsonProperty("country")]
    public string Country { get; set; }
}