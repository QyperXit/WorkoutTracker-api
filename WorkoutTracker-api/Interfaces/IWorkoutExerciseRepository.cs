using WorkoutTracker_api.DBContext.Dto;
using WorkoutTracker_api.Models;

namespace WorkoutTracker_api.Interfaces;

public interface IWorkoutExerciseRepository
{
    Task<bool> WorkoutBelongsToUserAsync(int workoutId, int userId);
    Task<IEnumerable<WorkoutExerciseDto>> GetAllWorkoutExercisesByWorkoutIdAsync(int workoutId);
    Task<WorkoutExerciseDto> GetWorkoutExerciseAsync(int workoutId, int exerciseId);
    Task AddWorkoutExerciseAsync(WorkoutExerciseDto workoutExerciseDto);
    Task<bool> UpdateWorkoutExerciseAsync(WorkoutExerciseDto workoutExerciseDto);
    Task<bool> PatchWorkoutExerciseAsync(int workoutId, int exerciseId, WorkoutExercisePatchDto patchDto);
    
    Task<bool> DeleteWorkoutExerciseAsync(int workoutId, int exerciseId);
    Task<bool> SaveChangesAsync();
}