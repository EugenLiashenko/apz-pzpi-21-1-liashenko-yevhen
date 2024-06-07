using System.ComponentModel.DataAnnotations;
using Core.Enums;

namespace Core.Entities;

public class Apartments
{
    [Key]
    public int Id { get; init; }
    
    public ApartmentsType Type { get; set; }
    
    public int Capacity { get; set; }
    
    public float StartingPrice { get; set; }
    
    public Availability Status { get; set; }
    
    public ApartmentsCondition Condition { get; set; }
}