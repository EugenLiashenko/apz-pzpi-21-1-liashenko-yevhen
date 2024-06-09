using System.ComponentModel.DataAnnotations;
using Core.Enums;

namespace Core.Entities;

public class User
{
    [Key]
    public int Id { get; init; }

    public string Name { get; set; } = string.Empty;

    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    public string PasswordHashed { get; set; } = string.Empty;
    
    public string PasswordSalt { get; set; } = string.Empty;

    public RoleEnum Role { get; set; }
    
    public List<Notification> Notifications { get; set; }

    public List<Booking> Bookings { get; set; }
}