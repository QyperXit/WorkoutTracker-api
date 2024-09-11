namespace WorkoutTracker_api.Models;

public class Workout
{
    public int Id { get; set; }
    public int UserId { get; set; }

    public DateTime Date { get; set; }
    public string Name { get; set; }
    public string Notes { get; set; }
    
    public User User { get; set; } // A Workout belongs to a User
    public ICollection<WorkoutExercise> WorkoutExercises { get; set; } = new List<WorkoutExercise>();

}