using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkoutTracker_api.Dto
{
    public class CreateExerciseEquipmentDto
    {
        public int ExerciseId { get; set; }
        public int EquipmentId { get; set; }
    }
}