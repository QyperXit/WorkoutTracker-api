using Microsoft.AspNetCore.Mvc;
using WorkoutTracker_api.DBContext.Dto;
using WorkoutTracker_api.Interfaces;
using WorkoutTracker_api.Mappers;

namespace WorkoutTracker_api.Controllers;


[Route("api/[controller]")]
[ApiController]
public class WorkoutExerciseController : ControllerBase
{
    private readonly IWorkoutExerciseRepository _workoutExerciseRepository;

    public WorkoutExerciseController( IWorkoutExerciseRepository workoutExerciseRepository )
    {
        _workoutExerciseRepository = workoutExerciseRepository;
    }

    [HttpGet("workout/{workoutId}/exercise/{exerciseId}")]
    public async Task<ActionResult<WorkoutExerciseDto>> GetWorkoutExerciseAsync(int workoutId, int exerciseId)
    {
        var workoutExercise = await _workoutExerciseRepository.GetWorkoutExerciseAsync(workoutId, exerciseId);
    
        if (workoutExercise == null)
        {
            return NotFound(new { Message = $"Workout exercise with Workout ID {workoutId} and Exercise ID {exerciseId} not found." });
        }
    
        return Ok(workoutExercise);
    }
    
    
    
    [HttpGet("workout/{workoutId}")]
    public async Task<ActionResult<IEnumerable<WorkoutExerciseDto>>> GetAllWorkoutExercisesByWorkoutId(int workoutId)
    {
        var workoutExercises = await _workoutExerciseRepository.GetAllWorkoutExercisesByWorkoutIdAsync(workoutId);

        if (workoutExercises == null || !workoutExercises.Any())
        {
            return NotFound(new { Message = $"No workout exercises found for Workout ID {workoutId}." });
        }

        return Ok(workoutExercises);
    }
    
    
}