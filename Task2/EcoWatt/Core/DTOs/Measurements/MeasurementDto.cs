namespace Core.DTOs.Measurements;

public class MeasurementDto: IModel
{
    public string Metrics { get; set; } = string.Empty;

    public string Value { get; set; } = string.Empty;

    public DateTime TimeSpan { get; set; }
    
    public int ApartmentsId { get; set; }
}