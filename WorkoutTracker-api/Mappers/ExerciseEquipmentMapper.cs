using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkoutTracker_api.Dto;
using WorkoutTracker_api.Models;

namespace WorkoutTracker_api.Mappers
{
    public class ExerciseEquipmentMapper
    {
   
    public static ExerciseEquipmentDto ToDto(ExerciseEquipment exerciseEquipment, string exerciseName, string equipmentName)
        {
            return new ExerciseEquipmentDto
            {
                ExerciseId = exerciseEquipment.ExerciseId,
                EquipmentId = exerciseEquipment.EquipmentId,
                ExerciseName = exerciseName,
                EquipmentName = equipmentName
            };
        }

        public static ExerciseEquipment ToModel(ExerciseEquipmentDto dto)
        {
            return new ExerciseEquipment
            {
                ExerciseId = dto.ExerciseId,
                EquipmentId = dto.EquipmentId
            };
        }
    
    }
}