using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkoutTracker_api.Dto
{
    public class ExerciseEquipmentDto
    {
    public int ExerciseId { get; set; }
    public string ExerciseName { get; set; } // Add name or relevant details
    public int EquipmentId { get; set; }
    public string EquipmentName { get; set; }
    }
}