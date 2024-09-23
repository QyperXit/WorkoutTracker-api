using WorkoutTracker_api.DBContext.Dto;
using WorkoutTracker_api.Models;

namespace WorkoutTracker_api.DBContext.Interfaces;

public interface IWorkoutRepository
{
    IEnumerable<WorkoutDto> GetWorkouts();
    Workout GetWorkout(int id);
    Workout CreateWorkout(Workout workout);
    
    bool UpdateWorkout(WorkoutUpdateDto workoutUpdateDto);  

    bool DeleteWorkout(int id);
    bool WorkoutExists(int id);
    bool SaveChanges();
}