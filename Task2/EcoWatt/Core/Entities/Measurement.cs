using System.ComponentModel.DataAnnotations;

namespace Core.Entities;

public class Measurement
{
    [Key]
    public int Id { get; set; }

    public string Metric { get; set; } = string.Empty;

    public string Value { get; set; } = string.Empty;

    public DateTime Timestamp { get; set; }
    
    public Apartments Apartment { get; set; }
}