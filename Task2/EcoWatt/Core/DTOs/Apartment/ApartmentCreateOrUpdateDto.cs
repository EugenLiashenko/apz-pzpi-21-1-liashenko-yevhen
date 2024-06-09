using Core.Entities;
using Core.Enums;

namespace Core.DTOs.Apartment;

public class ApartmentCreateOrUpdateDto: IModel
{
    public ApartmentsType Type { get; set; }
    
    public int Capacity { get; set; }
    
    public float StartingPrice { get; set; }
    
    public Availability Status { get; set; }
    
    public ApartmentsCondition Condition { get; set; }
}