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
        var workouts = _workoutRepository.GetWorkouts();
        if (workouts == null || !workouts.Any())
        {
            return NotFound();
        }
        return Ok(workouts);
    }
    
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetWorkoutById(int id)
    {
        var workout = _workoutRepository.GetWorkout(id);

        if (workout == null)
        {
            return NotFound(); 
        }

        // Map the workout to WorkoutDto and return it
        var workoutDto = WorkoutMapper.ToDto(workout);
        return Ok(workoutDto); // Return 200 with the workout data
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
        
        // map DTO to workout entity
        var workout = WorkoutMapper.ToEntity(workoutCreateDto);
        
        //call repository to create workout
        var createdWorkout = _workoutRepository.CreateWorkout(workout);
        
        //if return 201 if successful with created workout
        return CreatedAtAction(nameof(GetWorkoutById), new { id = createdWorkout.Id }, WorkoutMapper.ToDto(createdWorkout));
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
            return NoContent(); // 204 No Content is typically used for successful updates
        }

        return StatusCode(500, "An error occurred while updating the workout.");
    }

}