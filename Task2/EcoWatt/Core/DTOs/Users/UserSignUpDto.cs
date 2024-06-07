using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.Users;

public class UserSignUpDto: IModel
{
    public string Name { get; set; } = string.Empty;
    
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;
}