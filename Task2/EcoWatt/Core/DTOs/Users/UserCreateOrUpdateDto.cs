using System.ComponentModel.DataAnnotations;
using Core.Enums;

namespace Core.DTOs.Users;

public class UserCreateOrUpdateDto: IModel
{
    public string Name { get; set; } = string.Empty;

    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public RoleEnum Role { get; set; }
}