using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
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
    private readonly TokenService _tokenService;

    public UserController(IUserRepository userRepository,TokenService tokenService)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
    }
    // ** admin roles **
    // [Authorize(Roles = "Admin")]
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<User>))]
    public IActionResult GetUsers()
    {
        var users = _userRepository.GetUsers();
        
        if(!ModelState.IsValid)
            return BadRequest(ModelState);
        
        return Ok(users);
    }
    
    [Authorize]
    [HttpGet("{id}")]
    public ActionResult<UserDto> GetUserById(int id)
    {
        int currentUserId = GetUserIdFromToken();
        
        // Only allow users to access their own profile unless they're an admin
        if (currentUserId != id && !User.IsInRole("Admin"))
        {
            return Forbid();
        }

        var user = _userRepository.GetUserById(id);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(UserMapper.ToDto(user));
    }

    
// Create a new user
    [HttpPost("register")]
    public ActionResult CreateUser(UserCreateDto userCreateDto)
    {
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

    
    [Authorize]
    [HttpPut("{id}")]
    public IActionResult UpdateUser(int id, UserUpdateDto userUpdateDto)
    {
        
        int currentUserId = GetUserIdFromToken();

        if (currentUserId != id && !User.IsInRole("Admin"))
        {
            return Forbid();
        }
        
        var user = _userRepository.GetUserById(id);
        if (user == null)
        {
            return NotFound();
        }

        UserMapper.UpdateEntity(user, userUpdateDto);
        _userRepository.SaveChanges();

        return NoContent();
    }
    
    [Authorize]
    [HttpDelete("{userId}")]
    public IActionResult DeleteUser(int userId)
    {
        int currentUserId = GetUserIdFromToken();
        
        // Only allow users to delete their own profile unless they're an admin
        if (currentUserId != userId && !User.IsInRole("Admin"))
        {
            return Forbid();
        }

        if (!_userRepository.UserExists(userId))
        {
            return NotFound(new { message = "User not found" });
        }
        
        bool userDeleted = _userRepository.DeleteUser(userId);
        
        if (!userDeleted)
        {
            return StatusCode(500, new { message = "An error occurred while deleting the user." });
        }

        return NoContent();
    }
    
    
    [HttpPost("login")]
    public IActionResult Login(UserLoginDto loginDto)
    {
        var user = _userRepository.GetUserByEmail(loginDto.Email);
    
        if (user == null || !PasswordHelper.VerifyPassword(loginDto.Password, user.Password))
        {
            return Unauthorized(new { message = "Invalid email or password." });
        }

        var token = _tokenService.GenerateToken(user.Id.ToString(), user.Name);  // _tokenService is injected

        return Ok(new { Token = token });
    }



    private int GetUserIdFromToken()
{
    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    
    // Debugging: Log the extracted user ID
    Console.WriteLine($"User ID from token: {userId}");
    
    if (userId == null)
    {
        throw new UnauthorizedAccessException("User ID not found in token.");
    }
    
    return int.Parse(userId);
}

    
    
}