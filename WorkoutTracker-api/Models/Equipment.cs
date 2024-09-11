namespace WorkoutTracker_api.Models;

public class Equipment
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    // Many-to-many relationship with Exercise via the join entity ExerciseEquipment
    public ICollection<ExerciseEquipment> ExerciseEquipments { get; set; } = new List<ExerciseEquipment>();
}