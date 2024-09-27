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
        Task<EquipmentDto> CreateEquipmentAsync(CreateEquipmentDto createEquipmentDto); 

        Task<bool> CheckEquipmentExistsByNameAsync(string name);
        Task<bool> CheckEquipmentExistsAsync(int id);
        Task<bool> DeleteEquipmentAsync(int id);
        Task<bool> SaveChangesAsync();
            Task<bool> UpdateEquipmentAsync(int id, UpdateEquipmentDto updateEquipmentDto);

        
        
    }
}