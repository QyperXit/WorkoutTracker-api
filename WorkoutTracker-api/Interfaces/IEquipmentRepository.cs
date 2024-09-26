using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkoutTracker_api.Dto;

namespace WorkoutTracker_api.Interfaces
{
    public interface IEquipmentRepository
    {
        Task<IEnumerable<EquipmentDto>> GetAllEquipmentsAsync();
         Task<EquipmentDto> GetEquipmentByIdAsync(int id);
        // Task<EquipmentDto> CreateEquipmentAsync(EquipmentDto equipmentDto);
        // Task<bool> UpdateEquipmentAsync(EquipmentDto equipmentDto);
        // Task<bool> DeleteEquipmentAsync(int id);
        
    }
}