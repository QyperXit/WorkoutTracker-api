namespace WorkoutTracker_api.Models;

public class Exercise
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string PrimaryMuscleGroup { get; set; }
    
    // Many-to-many relationship with Equipment via the join entity ExerciseEquipment
    public ICollection<ExerciseEquipment> ExerciseEquipments { get; set; } = new List<ExerciseEquipment>();

    // Many-to-many relationship with WorkoutExercise
    public ICollection<WorkoutExercise> WorkoutExercises { get; set; } = new List<WorkoutExercise>();
}