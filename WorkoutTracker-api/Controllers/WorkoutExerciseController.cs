using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkoutTracker_api.DBContext.Dto;
using WorkoutTracker_api.Interfaces;
using WorkoutTracker_api.Mappers;

namespace WorkoutTracker_api.Controllers;

[Authorize]
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
        var userId = GetUserIdFromToken();
        
        // Verify workout belongs to user
        if (!await _workoutExerciseRepository.WorkoutBelongsToUserAsync(workoutId, userId))
        {
            return NotFound(); // Using NotFound instead of Forbidden for security
        }

        var workoutExercise = await _workoutExerciseRepository.GetWorkoutExerciseAsync(workoutId, exerciseId);
    
        if (workoutExercise == null)
        {
            return NotFound();
        }
    
        return Ok(workoutExercise);
    }
    
    
    
    [HttpGet("workout/{workoutId}")]
    public async Task<ActionResult<IEnumerable<WorkoutExerciseDto>>> GetAllWorkoutExercisesByWorkoutId(int workoutId)
    {
        var userId = GetUserIdFromToken();
        
        // Verify workout belongs to user
        if (!await _workoutExerciseRepository.WorkoutBelongsToUserAsync(workoutId, userId))
        {
            return NotFound();
        }

        var workoutExercises = await _workoutExerciseRepository.GetAllWorkoutExercisesByWorkoutIdAsync(workoutId);

        if (workoutExercises == null || !workoutExercises.Any())
        {
            return NotFound();
        }

        return Ok(workoutExercises);
    }
    

    [HttpPost]
    public async Task<ActionResult<WorkoutExerciseDto>> AddWorkoutExerciseAsync(WorkoutExerciseDto workoutExerciseDto)
    {
        var userId = GetUserIdFromToken();
        
        // Verify workout belongs to user
        if (!await _workoutExerciseRepository.WorkoutBelongsToUserAsync(workoutExerciseDto.WorkoutId, userId))
        {
            return NotFound();
        }

        await _workoutExerciseRepository.AddWorkoutExerciseAsync(workoutExerciseDto);
        return Ok(workoutExerciseDto);
    }
 

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateWorkoutExercise(int id, [FromBody] WorkoutExerciseDto workoutExerciseDto)
    {
        var userId = GetUserIdFromToken();
        
        // Verify workout belongs to user
        if (!await _workoutExerciseRepository.WorkoutBelongsToUserAsync(workoutExerciseDto.WorkoutId, userId))
        {
            return NotFound();
        }

        if (id != workoutExerciseDto.Id)
        {
            return BadRequest("ID mismatch.");
        }

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
        var userId = GetUserIdFromToken();
        
        // Verify workout belongs to user
        if (!await _workoutExerciseRepository.WorkoutBelongsToUserAsync(workoutId, userId))
        {
            return NotFound();
        }

        if (patchDto == null)
        {
            return BadRequest("Patch data is required.");
        }

        var result = await _workoutExerciseRepository.PatchWorkoutExerciseAsync(workoutId, exerciseId, patchDto);

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
    
    [HttpDelete("{workoutId}/{exerciseId}")]
    public async Task<IActionResult> DeleteWorkoutExercise(int workoutId, int exerciseId)
    {
        var userId = GetUserIdFromToken();
        
        // Verify workout belongs to user
        if (!await _workoutExerciseRepository.WorkoutBelongsToUserAsync(workoutId, userId))
        {
            return NotFound();
        }

        var result = await _workoutExerciseRepository.DeleteWorkoutExerciseAsync(workoutId, exerciseId);

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
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