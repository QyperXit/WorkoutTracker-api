using WorkoutTracker_api.DBContext.Dto;
using WorkoutTracker_api.Models;

namespace WorkoutTracker_api.Mappers;

public class WorkoutExerciseMapper
{
    public static WorkoutExerciseDto ToDto(WorkoutExercise workoutExercise)
    {
        return new WorkoutExerciseDto
        {
            Id = workoutExercise.Id,
            WorkoutId = workoutExercise.WorkoutId,
            ExerciseId = workoutExercise.ExerciseId,
            ExerciseName = workoutExercise.Exercise?.Name, // If Exercise is included
            Sets = workoutExercise.Sets,
            Reps = workoutExercise.Reps,
            Weight = workoutExercise.Weight,
            RestTimeSeconds = workoutExercise.RestTimeSeconds
        };
    }
}