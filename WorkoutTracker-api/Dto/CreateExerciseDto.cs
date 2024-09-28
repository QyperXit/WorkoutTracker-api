using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkoutTracker_api.Dto
{
    public class CreateExerciseDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string PrimaryMuscleGroup { get; set; }
    }
}