namespace WorkoutTracker_api.Models;

public class WorkoutExercise
{
    public int Id { get; set; }
    public int WorkoutId { get; set; }

    public int ExerciseId { get; set; }
    public int Sets { get; set; }
    public int Reps { get; set; }
    public int Weight { get; set; }
    public int RestTimeSeconds { get; set; }  
    
    public Workout Workout { get; set; } // WorkoutExercise belongs to a Workout
    public Exercise Exercise { get; set; } // WorkoutExercise belongs to an Exercise
}