namespace WorkoutTracker_api.DBContext.Dto;

public class WorkoutExercisePatchDto
{
    public int? Sets { get; set; }
    public int? Reps { get; set; }
    public int? Weight { get; set; }
    public int? RestTimeSeconds { get; set; }
}