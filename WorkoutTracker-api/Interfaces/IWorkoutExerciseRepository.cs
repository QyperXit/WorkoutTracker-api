using WorkoutTracker_api.DBContext.Dto;
using WorkoutTracker_api.Models;

namespace WorkoutTracker_api.Interfaces;

public interface IWorkoutExerciseRepository
{
    Task<IEnumerable<WorkoutExerciseDto>> GetAllWorkoutExercisesByWorkoutIdAsync(int workoutId);
    Task<WorkoutExerciseDto> GetWorkoutExerciseAsync(int workoutId, int exerciseId);
    // Task<WorkoutExercise> AddWorkoutExerciseAsync(WorkoutExercise workoutExercise);
    // Task<bool> DeleteWorkoutExerciseAsync(int workoutId, int exerciseId);
    // Task<bool> SaveChangesAsync();
}