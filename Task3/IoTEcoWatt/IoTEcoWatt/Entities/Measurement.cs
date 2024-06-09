using Newtonsoft.Json;

namespace IoTEcoWatt.Entities;

public class Measurement
{
    [JsonProperty("metrics")] 
    public string Metrics { get; init; } = string.Empty;

    [JsonProperty("value")] 
    public string Value { get; init; } = string.Empty;

    [JsonProperty("timeSpan")] 
    public DateTime TimeSpan { get; init; }

    [JsonProperty("apartmentsId")]
    public int ApartmentsId { get; init; }
}