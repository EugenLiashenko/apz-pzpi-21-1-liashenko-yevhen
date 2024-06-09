using System.ComponentModel.DataAnnotations;

namespace Core.Entities;

public class Booking
{
    [Key]
    public int Id { get; init; }

    public User User { get; set; } = new User();

    public Apartments Apartment { get; set; } = new Apartments();

    public DateOnly StartingDate { get; set; }
    
    public DateOnly EndingDate { get; set; }
    
    public float PriceForManagement { get; set; }
    
    public float TotalPrice { get; set; }
}