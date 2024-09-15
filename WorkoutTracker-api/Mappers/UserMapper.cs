using WorkoutTracker_api.DBContext.Dto;
using WorkoutTracker_api.Models;

namespace WorkoutTracker_api.DBContext.Mapper;

public class UserMapper
{
    
    public static UserDto ToDto(User user)
    {
        return new UserDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email
        };
    }
    
    
    public static User ToEntity(UserCreateDto userCreateDto)
    {
        return new User
        {
            Name = userCreateDto.Name,
            Email = userCreateDto.Email,
            Password = PasswordHelper.HashPassword(userCreateDto.Password) // Use PasswordHelper to hash password
        };
    }
    
    public static void UpdateEntity(User user, UserUpdateDto userUpdateDto)
    {
        user.Name = userUpdateDto.Name;
        user.Email = userUpdateDto.Email;
        if (!string.IsNullOrEmpty(userUpdateDto.Password))
        {
            user.Password = PasswordHelper.HashPassword(userUpdateDto.Password);
        }
    }
    
    
    
}