using WorkoutTracker_api.DBContext.Dto;
using WorkoutTracker_api.Models;

namespace WorkoutTracker_api.DBContext.Mapper;

public class WorkoutMapper
{
    public static WorkoutDto ToDto(Workout workout)
    {
        if (workout == null) return null;

        return new WorkoutDto
        {
            Id = workout.Id,
            UserId = workout.UserId,
            Date = workout.Date,
            Name = workout.Name,
            Notes = workout.Notes,
            WorkoutExercises = workout.WorkoutExercises.Select(we => new WorkoutExerciseDto
            {
                Id = we.Id,
                WorkoutId = we.WorkoutId,
                ExerciseId = we.ExerciseId,
                ExerciseName = we.Exercise?.Name, // Map ExerciseName from Exercise entity
                Sets = we.Sets,
                Reps = we.Reps,
                Weight = we.Weight,
                RestTimeSeconds = we.RestTimeSeconds
            }).ToList()
        };
    }
    
    public static Workout ToEntity(WorkoutCreateDto workoutCreateDto)
    {
        return new Workout
        {
            UserId = workoutCreateDto.UserId,
            Date = workoutCreateDto.Date,
            Name = workoutCreateDto.Name,
            Notes = workoutCreateDto.Notes,
            WorkoutExercises = workoutCreateDto.WorkoutExercises.Select(we => new WorkoutExercise
            {
                ExerciseId = we.ExerciseId,
                Sets = we.Sets,
                Reps = we.Reps,
                Weight = we.Weight,
                RestTimeSeconds = we.RestTimeSeconds
            }).ToList()
        };
    }
}