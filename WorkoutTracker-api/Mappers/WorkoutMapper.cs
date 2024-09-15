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
    
    
    //
   public static void UpdateModelFromDto(WorkoutUpdateDto dto, Workout workout)
        {
            workout.UserId = dto.UserId;
            workout.Date = dto.Date;
            workout.Name = dto.Name;
            workout.Notes = dto.Notes;

            // Update existing WorkoutExercises, add new ones, and remove old ones
            var updatedExercises = new List<WorkoutExercise>();

            foreach (var exerciseDto in dto.WorkoutExercises)
            {
                var existingExercise = workout.WorkoutExercises
                    .FirstOrDefault(we => we.WorkoutId == workout.Id && we.ExerciseId == exerciseDto.ExerciseId);

                if (existingExercise != null)
                {
                    // Update existing
                    existingExercise.Sets = exerciseDto.Sets;
                    existingExercise.Reps = exerciseDto.Reps;
                    existingExercise.Weight = exerciseDto.Weight;
                    existingExercise.RestTimeSeconds = exerciseDto.RestTimeSeconds;
                    updatedExercises.Add(existingExercise);
                }
                else
                {
                    // Add new
                    var newExercise = new WorkoutExercise
                    {
                        WorkoutId = workout.Id,
                        ExerciseId = exerciseDto.ExerciseId,
                        Sets = exerciseDto.Sets,
                        Reps = exerciseDto.Reps,
                        Weight = exerciseDto.Weight,
                        RestTimeSeconds = exerciseDto.RestTimeSeconds
                    };
                    updatedExercises.Add(newExercise);
                }
            }

            // Remove exercises not in the DTO
            var exercisesToRemove = workout.WorkoutExercises
                .Where(we => !dto.WorkoutExercises.Any(dtoWe => dtoWe.ExerciseId == we.ExerciseId))
                .ToList();

            foreach (var exerciseToRemove in exercisesToRemove)
            {
                workout.WorkoutExercises.Remove(exerciseToRemove);
            }

            // Add new exercises
            foreach (var newExercise in updatedExercises.Where(ue => !workout.WorkoutExercises.Any(we => we.ExerciseId == ue.ExerciseId)))
            {
                workout.WorkoutExercises.Add(newExercise);
            }
        }
    
    
    
}