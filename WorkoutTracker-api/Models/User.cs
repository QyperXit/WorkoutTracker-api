namespace WorkoutTracker_api.Models;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    
    //a User has many Workouts
    public ICollection<Workout> Workouts { get; set; } = new List<Workout>();

}