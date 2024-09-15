using WorkoutTracker_api.DBContext.Dto;
using WorkoutTracker_api.Models;

namespace WorkoutTracker_api.DBContext.Interfaces;

public interface IWorkoutRepository
{
    IEnumerable<WorkoutDto> GetWorkouts();
    Workout GetWorkout(int id);
    Workout CreateWorkout(Workout workout);
    // bool UpdateWorkout(Workout workout);
    bool UpdateWorkout(WorkoutUpdateDto workoutUpdateDto);  // Update the parameter type to WorkoutUpdateDto

    bool DeleteWorkout(Workout workout);
    bool WorkoutExists(int id);
    bool SaveChanges();
}