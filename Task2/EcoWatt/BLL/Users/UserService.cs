using BLL.Validation;
using Core.DTOs.Users;
using Core.Entities;
using Core.Enums;
using CsvHelper.Configuration.Attributes;
using DAL.Data;
using Microsoft.EntityFrameworkCore;

namespace BLL.Users;

public class UserService: IUserService
{
    private readonly DataContext _dataContext;
    private readonly IPasswordService _passwordService;

    public UserService(DataContext dbContext, IPasswordService passwordService)
    {
        _dataContext = dbContext;
        _passwordService = passwordService;
    }

    public async Task<List<User>> GetAllAsync()
    {
        return await _dataContext.Users.ToListAsync();
    }

    public async Task<User>? GetUserByIdAsync(int id)
    {
        return await _dataContext.Users.FindAsync(id);
    }

    public async Task CreateUserAsync(UserCreateOrUpdateDto user)
    {
        if (user is null)
        {
            throw new ArgumentNullException(nameof(user));
        }

        var foundedUser = await _dataContext.Users.FirstOrDefaultAsync(u => u.Email == user.Email);

        if (foundedUser is not null)
        {
            throw new ArgumentException();
        }

        var (hashedPassword, salt) = _passwordService.HashPassword(user.Password);

        _dataContext.Users.Add(new User
        {
            Name = user.Name,
            Role = user.Role,
            Email = user.Email,
            PasswordHashed = hashedPassword,
            PasswordSalt = salt,
            Bookings = new List<Booking>(),
            Notifications = new List<Notification>()
        });

        await _dataContext.SaveChangesAsync();
    }

    public async Task UpdateUserAsync(int id, UserCreateOrUpdateDto updateUserModel)
    {
        if (updateUserModel is null)
        {
            throw new ArgumentNullException(nameof(updateUserModel));
        }

        var dbUser = await _dataContext.FindAsync<User>(id);

        if (dbUser is null)
        {
            throw new ArgumentNullException(nameof(updateUserModel));
        }

        dbUser.Name = updateUserModel.Name;
        dbUser.Email = updateUserModel.Email;
        dbUser.Role = updateUserModel.Role;

        _dataContext.Users.Update(dbUser);

        await _dataContext.SaveChangesAsync();
    }

    public async Task DeleteUserAsync(int id)
    {
        var user = await _dataContext.Users.FindAsync(id);
        if (user != null)
        {
            _dataContext.Users.Remove(user);
            await _dataContext.SaveChangesAsync();
        }
    }

    public async Task<bool> SignUpAsync(UserSignUpDto signUpModel)
    {
        var existingUser = await _dataContext.Users.FirstOrDefaultAsync(u => u.Email == signUpModel.Email);

        if (existingUser != null)
        {
            return false;
        }

        var (hash, salt) = _passwordService.HashPassword(signUpModel.Password);

        var newUser = new User
        {
            Name = signUpModel.Name,
            Role = RoleEnum.User,
            Email = signUpModel.Email,
            PasswordHashed = hash,
            PasswordSalt = salt,
            Bookings = new List<Booking>(),
            Notifications = new List<Notification>()
        };

        await _dataContext.Users.AddAsync(newUser);
        await _dataContext.SaveChangesAsync();

        return true;
    }

    public async Task<User>? LoginAsync(UserLoginDto userLoginDto)
    {
        var user = await _dataContext.Users.FirstOrDefaultAsync(u => u.Email == userLoginDto.Email);

        if (user is not null && _passwordService.IsValid(userLoginDto.Password, user.PasswordHashed, user.PasswordSalt))
        {
            return user;
        }

        return null;
    }
}