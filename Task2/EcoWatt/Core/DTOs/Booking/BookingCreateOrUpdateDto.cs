using Core.Entities;

namespace Core.DTOs.Booking;

public class BookingCreateOrUpdateDto: IModel
{
    public int UserId { get; set; }

    public int ApartmentId { get; set; }

    public DateTime StartingDate { get; set; }
    
    public DateTime EndingDate { get; set; }
    
    public float PriceForManagement { get; set; }
    
    public float TotalPrice { get; set; }
}