namespace WorkoutTracker_api.DBContext.Dto;

public class WorkoutUpdateDto
{
    public int Id { get; set; }  // Id of the workout to update
    public int UserId { get; set; }
    public DateTime Date { get; set; }
    public string Name { get; set; }
    public string Notes { get; set; }
    public List<WorkoutExerciseUpdateDto> WorkoutExercises { get; set; } = new List<WorkoutExerciseUpdateDto>();
}