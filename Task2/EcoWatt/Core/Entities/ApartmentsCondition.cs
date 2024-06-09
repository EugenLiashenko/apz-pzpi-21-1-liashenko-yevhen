using System.ComponentModel.DataAnnotations;

namespace Core.Entities;

public class ApartmentsCondition
{
    [Key]
    public int Id { get; init; }
    
    public float MinTemperature { get; set; }
    
    public float MaxTemperature { get; set; }
    
    /// <summary>
    /// Max usage of water defined for every room in LITERS
    /// </summary>
    public float MaxWaterUsage { get; set; }
    
    /// <summary>
    /// Max usage of electricity defined for every room in WATTS
    /// </summary>
    public float MaxElectricityUsage { get; set; }
}