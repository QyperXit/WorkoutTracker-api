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

    public async Task AddWorkoutExerciseAsync(WorkoutExerciseDto workoutExerciseDto)
    {
        var workoutExercise = new WorkoutExercise
        {
            WorkoutId = workoutExerciseDto.WorkoutId,
            ExerciseId = workoutExerciseDto.ExerciseId,
            Sets = workoutExerciseDto.Sets,
            Reps = workoutExerciseDto.Reps,
            Weight = workoutExerciseDto.Weight,
            RestTimeSeconds = workoutExerciseDto.RestTimeSeconds
        };

        await _context.WorkoutExercises.AddAsync(workoutExercise);
        await SaveChangesAsync();
    }

    public async Task<bool> UpdateWorkoutExerciseAsync(WorkoutExerciseDto workoutExerciseDto)
    {
        // Find the existing WorkoutExercise using both keys
        var existingWorkoutExercise = await _context.WorkoutExercises
            .FirstOrDefaultAsync(we => we.WorkoutId == workoutExerciseDto.WorkoutId && 
                                       we.ExerciseId == workoutExerciseDto.ExerciseId);
    
        if (existingWorkoutExercise == null)
        {
            return false; 
        }

        // Update the properties
        existingWorkoutExercise.Sets = workoutExerciseDto.Sets;
        existingWorkoutExercise.Reps = workoutExerciseDto.Reps;
        existingWorkoutExercise.Weight = workoutExerciseDto.Weight;
        existingWorkoutExercise.RestTimeSeconds = workoutExerciseDto.RestTimeSeconds;

        await SaveChangesAsync();
        return true;
    }
    
    public async Task<bool> PatchWorkoutExerciseAsync(int workoutId, int exerciseId, WorkoutExercisePatchDto patchDto)
    {
        var existingWorkoutExercise = await _context.WorkoutExercises
            .FirstOrDefaultAsync(we => we.WorkoutId == workoutId && we.ExerciseId == exerciseId);

        if (existingWorkoutExercise == null)
        {
            return false;
        }

        // Update only the properties that are not null in the patchDto
        if (patchDto.Sets.HasValue)
            existingWorkoutExercise.Sets = patchDto.Sets.Value;
        if (patchDto.Reps.HasValue)
            existingWorkoutExercise.Reps = patchDto.Reps.Value;
        if (patchDto.Weight.HasValue)
            existingWorkoutExercise.Weight = patchDto.Weight.Value;
        if (patchDto.RestTimeSeconds.HasValue)
            existingWorkoutExercise.RestTimeSeconds = patchDto.RestTimeSeconds.Value;

        await SaveChangesAsync();
        return true;
    }
    
    public async Task<bool> DeleteWorkoutExerciseAsync(int workoutId, int exerciseId)
    {
        var workoutExercise = await _context.WorkoutExercises
            .FirstOrDefaultAsync(we => we.WorkoutId == workoutId && we.ExerciseId == exerciseId);

        if (workoutExercise == null)
        {
            return false;
        }

        _context.WorkoutExercises.Remove(workoutExercise);
        await SaveChangesAsync();
        return true;
    }
    
    public async Task<bool> WorkoutBelongsToUserAsync(int workoutId, int userId)
    {
        return await _context.Workouts
            .AnyAsync(w => w.Id == workoutId && w.UserId == userId);
    }


    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0; 
    }
}