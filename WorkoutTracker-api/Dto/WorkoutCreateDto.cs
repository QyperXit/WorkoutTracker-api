namespace WorkoutTracker_api.DBContext.Dto;

public class WorkoutCreateDto
{
    public string Name { get; set; }
    public string Notes { get; set; }
    public DateTime Date { get; set; }
    public List<WorkoutExerciseDto> WorkoutExercises { get; set; } // Include exercises 
}