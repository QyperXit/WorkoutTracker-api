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
}