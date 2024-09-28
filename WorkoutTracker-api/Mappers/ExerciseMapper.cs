using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkoutTracker_api.Dto;
using WorkoutTracker_api.Models;

namespace WorkoutTracker_api.Mappers
{
    public class ExerciseMapper
    {
        
        public static ExerciseDto ToDto(Exercise exercise)
        {
            if (exercise == null) return null;

            return new ExerciseDto
            {
                Id = exercise.Id,
                Name = exercise.Name,
                Description = exercise.Description,
                PrimaryMuscleGroup = exercise.PrimaryMuscleGroup
            };
        }

        public static Exercise ToModel(ExerciseDto exerciseDto)
        {
            if (exerciseDto == null) return null;

            return new Exercise
            {
                Id = exerciseDto.Id,
                Name = exerciseDto.Name,
                Description = exerciseDto.Description,
                PrimaryMuscleGroup = exerciseDto.PrimaryMuscleGroup
            };
        }
    }
}