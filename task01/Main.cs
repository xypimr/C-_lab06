using Newtonsoft.Json;

namespace task01;

public class Main
{
    public double _temp { get; set; }
    [JsonProperty("temp")]
    public double Temp
    {
        get
        {
            return _temp;
        }
        set
        {
            _temp = value - 273;
        }
    }
}