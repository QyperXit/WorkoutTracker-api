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
    [HttpGet("{userId}")]
    public ActionResult<UserDto> GetUserById(int userId)
    {
        var user = _userRepository.GetUserById(userId);
        if (user == null)
        {
            return NotFound();
        }

        return Ok(UserMapper.ToDto(user));
    }
    
    
    
}