using Core.DTOs.Users;
using Core.Entities;

namespace BLL.Users;

public interface IUserService
{
    Task<List<User>> GetAllAsync();

    Task<User>? GetUserByIdAsync(int id);

    Task CreateUserAsync(UserCreateOrUpdateDto user);

    Task UpdateUserAsync(int id, UserCreateOrUpdateDto user);

    Task DeleteUserAsync(int id);

    Task<User> LoginAsync(UserLoginDto userLoginDto);

    Task<bool>? SignUpAsync(UserSignUpDto signUpDto);
}