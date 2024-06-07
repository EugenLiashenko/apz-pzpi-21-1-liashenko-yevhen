using Core.Entities;

namespace Core.DTOs.Booking;

public class BookingCreateOrUpdateDto: IModel
{
    public User User { get; set; }

    public Apartments Apartment { get; set; }

    public DateOnly StartingDate { get; set; }
    
    public DateOnly EndingDate { get; set; }
    
    public float PriceForManagement { get; set; }
    
    public float TotalPrice { get; set; }
}