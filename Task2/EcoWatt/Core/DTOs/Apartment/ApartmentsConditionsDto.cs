namespace Core.DTOs.Apartment;

public class ApartmentsConditionsDto: IModel
{
    public float Temperature { get; set; }
    
    public float WaterUsage { get; set; }
    
    public float ElectricityUsage { get; set; }
}