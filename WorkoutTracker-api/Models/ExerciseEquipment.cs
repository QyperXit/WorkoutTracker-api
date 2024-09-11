namespace WorkoutTracker_api.Models;

public class ExerciseEquipment
{
    public int ExerciseId { get; set; }
    public Exercise Exercise { get; set; }

    public int EquipmentId { get; set; }
    public Equipment Equipment { get; set; }
}