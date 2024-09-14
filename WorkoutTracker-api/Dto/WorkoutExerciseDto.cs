namespace WorkoutTracker_api.DBContext.Dto;

public class WorkoutExerciseDto
{
    public int Id { get; set; }
    public int WorkoutId { get; set; }
    public int ExerciseId { get; set; }
    public string ExerciseName { get; set; } // Name of the exercise
    public int Sets { get; set; }
    public int Reps { get; set; }
    public int Weight { get; set; }
    public int RestTimeSeconds { get; set; } 

}