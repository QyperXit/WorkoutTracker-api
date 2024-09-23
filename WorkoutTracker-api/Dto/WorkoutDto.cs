namespace WorkoutTracker_api.DBContext.Dto;

public class WorkoutDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public DateTime Date { get; set; }
    public string Name { get; set; }
    public string Notes { get; set; }
    public ICollection<WorkoutExerciseDto> WorkoutExercises { get; set; } // Related exercises
}