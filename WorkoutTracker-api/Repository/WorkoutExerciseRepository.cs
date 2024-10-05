using Microsoft.EntityFrameworkCore;
using WorkoutTracker_api.DBContext;
using WorkoutTracker_api.DBContext.Dto;
using WorkoutTracker_api.Interfaces;
using WorkoutTracker_api.Models;


namespace WorkoutTracker_api.Repository;

public class WorkoutExerciseRepository : IWorkoutExerciseRepository
{
    private readonly DataContext _context;

    public WorkoutExerciseRepository(DataContext _context)
    {
        this._context = _context;
    }

    // public async Task<WorkoutExercise> GetWorkoutExerciseAsync(int workoutId, int exerciseId)
    // {
    //     return await _context.WorkoutExercises.FirstOrDefaultAsync(we => we.Id == workoutId && we.ExerciseId == exerciseId);
    // }

    public async Task<IEnumerable<WorkoutExerciseDto>> GetAllWorkoutExercisesByWorkoutIdAsync(int workoutId)
    {
        return await _context.WorkoutExercises
            .Where(we => we.WorkoutId == workoutId)
            .Include(we => we.Exercise) // Eager load the Exercise for ExerciseName
            .Select(we => new WorkoutExerciseDto
            {
                Id = we.Id, // Include the Id here too if you want it
                WorkoutId = we.WorkoutId,
                ExerciseId = we.ExerciseId,
                ExerciseName = we.Exercise.Name, // Ensure Exercise is not null with Include
                Sets = we.Sets,
                Reps = we.Reps,
                Weight = we.Weight,
                RestTimeSeconds = we.RestTimeSeconds
            })
            .ToListAsync();
    }

    public async Task<WorkoutExerciseDto> GetWorkoutExerciseAsync(int workoutId, int exerciseId)
    {
        return await _context.WorkoutExercises
            .Where(we => we.WorkoutId == workoutId && we.ExerciseId == exerciseId)
            .Include(we => we.Exercise)
            .Select(we => new WorkoutExerciseDto
            {
                Id = we.Id,
                WorkoutId = we.WorkoutId,
                ExerciseId = we.ExerciseId,
                ExerciseName = we.Exercise.Name,
                Sets = we.Sets,
                Reps = we.Reps,
                Weight = we.Weight,
                RestTimeSeconds = we.RestTimeSeconds
            })
            .FirstOrDefaultAsync();
    }
}