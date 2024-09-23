namespace WorkoutTracker_api.DBContext.Dto;

public class WorkoutExerciseCreateDto
{
    public int ExerciseId { get; set; }
    public int Sets { get; set; }
    public int Reps { get; set; }
    public int Weight { get; set; }
    public int RestTimeSeconds { get; set; }
}