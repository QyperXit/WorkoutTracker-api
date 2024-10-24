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
public class WorkoutController : ControllerBase
{
    private readonly IWorkoutRepository _workoutRepository;

    public WorkoutController(IWorkoutRepository workoutRepository)
    {
        _workoutRepository = workoutRepository;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<WorkoutDto>))]
    public IActionResult GetWorkouts()
    {
        // Get current user's ID from the token
        var userId = GetUserIdFromToken();
        
        // Get only workouts belonging to the current user
        var workouts = _workoutRepository.GetWorkoutsByUserId(userId);
        
        if (workouts == null || !workouts.Any())
        {
            return NotFound();
        }
        return Ok(workouts);
    }
    
    
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public IActionResult GetWorkoutById(int id)
    {
        var userId = GetUserIdFromToken();
        var workout = _workoutRepository.GetWorkout(id);

        if (workout == null)
        {
            return NotFound(); 
        }

        // Check if the workout belongs to the current user
        if (workout.UserId != userId)
        {
            return Forbid();
        }

        var workoutDto = WorkoutMapper.ToDto(workout);
        return Ok(workoutDto); 
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult CreateWorkout([FromBody] WorkoutCreateDto workoutCreateDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var userId = GetUserIdFromToken();
        
        // Map DTO to workout entity and set the current user's ID
        var workout = WorkoutMapper.ToEntity(workoutCreateDto);
        workout.UserId = userId; // Ensure the workout is associated with the current user
        
        var createdWorkout = _workoutRepository.CreateWorkout(workout);
        
        return CreatedAtAction(
            nameof(GetWorkoutById), 
            new { id = createdWorkout.Id }, 
            WorkoutMapper.ToDto(createdWorkout)
        );
    }
    
    [HttpPut("{id}")]
    public IActionResult UpdateWorkout(int id, [FromBody] WorkoutUpdateDto workoutUpdateDto)
    {
        if (workoutUpdateDto == null || workoutUpdateDto.Id != id)
        {
            return BadRequest("Invalid input or ID mismatch.");
        }

        if (!_workoutRepository.WorkoutExists(id))
        {
            return NotFound($"Workout with ID {id} not found.");
        }

        if (_workoutRepository.UpdateWorkout(workoutUpdateDto))
        {
            return NoContent(); 
        }

        return StatusCode(500, "An error occurred while updating the workout.");
    }
    
    [HttpDelete("{id}")]
    public IActionResult DeleteWorkout(int id)
    {
        if (!_workoutRepository.WorkoutExists(id))
        {
            return NotFound($"Workout with ID {id} not found.");
        }

        var success = _workoutRepository.DeleteWorkout(id);

        if (success)
        {
            return NoContent();
        }

        return StatusCode(500, "An error occurred while deleting the workout.");
    }
    
    private int GetUserIdFromToken()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null)
        {
            throw new UnauthorizedAccessException("User ID not found in token.");
        }
        return int.Parse(userId);
    }

}