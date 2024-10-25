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
    

    [HttpPost]
    public async Task<ActionResult<WorkoutExerciseDto>> AddWorkoutExerciseAsync(WorkoutExerciseDto workoutExerciseDto)
    {
        await _workoutExerciseRepository.AddWorkoutExerciseAsync(workoutExerciseDto);
        return Ok(workoutExerciseDto);
    }
 

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateWorkoutExercise(int id, [FromBody] WorkoutExerciseDto workoutExerciseDto)
    {
        // Validate the incoming DTO
        if (id != workoutExerciseDto.Id) // You might want to validate against composite keys
        {
            return BadRequest("ID mismatch.");
        }

        // Call the repository method to update
        var result = await _workoutExerciseRepository.UpdateWorkoutExerciseAsync(workoutExerciseDto);
    
        if (!result)
        {
            return NotFound(); 
        }

        return NoContent(); 
    }

    [HttpPatch("{workoutId}/{exerciseId}")]
    public async Task<IActionResult> PatchWorkoutExercise(int workoutId, int exerciseId, [FromBody] WorkoutExercisePatchDto patchDto)
    {
        if (patchDto == null)
        {
            return BadRequest("Patch data is required.");
        }

        var result = await _workoutExerciseRepository.PatchWorkoutExerciseAsync(workoutId, exerciseId, patchDto);

        if (!result)
        {
            return NotFound($"WorkoutExercise with WorkoutId {workoutId} and ExerciseId {exerciseId} not found.");
        }

        return NoContent();
    }
    
    [HttpDelete("{workoutId}/{exerciseId}")]
    public async Task<IActionResult> DeleteWorkoutExercise(int workoutId, int exerciseId)
    {
        var result = await _workoutExerciseRepository.DeleteWorkoutExerciseAsync(workoutId, exerciseId);

        if (!result)
        {
            return NotFound($"WorkoutExercise with WorkoutId {workoutId} and ExerciseId {exerciseId} not found.");
        }

        return NoContent();
    }
    

    
}