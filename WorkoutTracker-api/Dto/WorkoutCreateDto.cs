namespace WorkoutTracker_api.DBContext.Dto;

public class WorkoutCreateDto
{
    
    public int UserId { get; set; }
    public DateTime Date { get; set; }
    public string Name { get; set; }
    public string Notes { get; set; }
    public List<WorkoutExerciseCreateDto> WorkoutExercises { get; set; } = new List<WorkoutExerciseCreateDto>();
}