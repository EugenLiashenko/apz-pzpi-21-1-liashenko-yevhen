namespace IoTEcoWatt.Entities;

public class Apartments
{
    public int Id { get; init; }
    
    public int Type { get; set; }
    
    public int Capacity { get; set; }
    
    public float StartingPrice { get; set; }
    
    public int Status { get; set; }
    
    public int Condition { get; set; }
}