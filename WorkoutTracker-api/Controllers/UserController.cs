using Microsoft.AspNetCore.Mvc;
using WorkoutTracker_api.DBContext.Dto;
using WorkoutTracker_api.DBContext.Interfaces;
using WorkoutTracker_api.DBContext.Mapper;
using WorkoutTracker_api.Models;

namespace WorkoutTracker_api.DBContext.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpGet]
    // [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(200, Type = typeof(IEnumerable<User>))]
    public IActionResult GetUsers()
    {
        var users = _userRepository.GetUsers();
        
        if(!ModelState.IsValid)
            return BadRequest(ModelState);
        
        return Ok(users);
    }
    
    // Get user by ID
    [HttpGet("{id}")]
    public ActionResult<UserDto> GetUserById(int id)
    {
        var user = _userRepository.GetUserById(id);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(UserMapper.ToDto(user));
    }

    
// Create a new user
    [HttpPost]
    public ActionResult CreateUser(UserCreateDto userCreateDto)
    {
        // Basic input validation
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            // Check if a user with the provided email already exists
            if (_userRepository.UserExists(userCreateDto.Email))
            {
                return Conflict(new { message = "A user with this email already exists." });
            }

            // Convert the DTO to an entity and create the user
            var user = UserMapper.ToEntity(userCreateDto);
            bool created = _userRepository.CreateUser(user);

            // Check if the creation was successful
            if (!created)
            {
                return StatusCode(500, new { message = "An error occurred while creating the user." });
            }

            // Map the user back to a DTO and return a 201 Created response
            var userDto = UserMapper.ToDto(user);
            return CreatedAtAction(nameof(GetUserById), new { id = userDto.Id }, userDto);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An unexpected error occurred." });
        }
    }

    
    [HttpPut("{userId}")]
    public IActionResult UpdateUser(int id, UserUpdateDto userUpdateDto)
    {
        var user = _userRepository.GetUserById(id);
        if (user == null)
        {
            return NotFound();
        }

        UserMapper.UpdateEntity(user, userUpdateDto);
        _userRepository.SaveChanges();

        return NoContent();
    }
    
    [HttpDelete("{userId}")]
    public IActionResult DeleteUser(int userId)
    {
        // Check if the user exists in the repository
        if (!_userRepository.UserExists(userId))
        {
            return NotFound(new { message = "User not found" });
        }
        
        
        // Delete the user
        bool UserDeleted = _userRepository.DeleteUser(userId);
        

        // Check if deletion was successful
        if (!UserDeleted)
        {
            return StatusCode(500, new { message = "An error occurred while deleting the user." });
        }

        return NoContent(); // Successfully deleted, return 204 No Content
    }
        

    
    
    
}