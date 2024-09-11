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
    
}