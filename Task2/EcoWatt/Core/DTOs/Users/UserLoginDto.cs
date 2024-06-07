using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.Users;

public class UserLoginDto: IModel
{
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;
}