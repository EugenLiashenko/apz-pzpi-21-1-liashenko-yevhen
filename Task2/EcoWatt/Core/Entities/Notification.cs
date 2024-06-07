using System.ComponentModel.DataAnnotations;

namespace Core.Entities;

public class Notification
{
    [Key]
    public int Id { get; set; }

    public string Message { get; set; } = string.Empty;

    public bool IsRead { get; set; }

    public User User { get; set; }
}