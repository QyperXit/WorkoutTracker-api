using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkoutTracker_api.Dto;
using WorkoutTracker_api.Models;

namespace WorkoutTracker_api.Mappers
{
    public class EquipmentMapper
    {
        public static EquipmentDto ToDto(Equipment equipment)
        {
            if (equipment == null) return null;

            return new EquipmentDto
            {
                Id = equipment.Id,
                Name = equipment.Name,
                Description = equipment.Description
            };
        }

        public static Equipment ToModel(EquipmentDto equipmentDto)
        {
            if (equipmentDto == null) return null;

            return new Equipment
            {
                Id = equipmentDto.Id,
                Name = equipmentDto.Name,
                Description = equipmentDto.Description
            };
        }
    }
}