using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WorkoutTracker_api.DBContext;
using WorkoutTracker_api.Dto;
using WorkoutTracker_api.Interfaces;
using WorkoutTracker_api.Models;


namespace WorkoutTracker_api.Repository
{
    public class EquipmentRepository : IEquipmentRepository
    {
        private readonly DataContext _context;
        public EquipmentRepository(DataContext _context)
        {
            this._context = _context;
            
        }

         public async Task<bool> CheckEquipmentExistsAsync(int id)
        {
            return await _context.Equipments.AnyAsync(e => e.Id == id);
        }

           public async Task<bool> DeleteEquipmentAsync(int id)
        {
            var equipment = await _context.Equipments.FindAsync(id);
            if (equipment == null)
                return false; // Return false if not found

            _context.Equipments.Remove(equipment); // Remove the equipment
            return await SaveChangesAsync(); // Save changes and return success
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0; // Return true if save was successful
        }

        public async Task<IEnumerable<EquipmentDto>> GetAllEquipmentsAsync()
        {
            var equipments = await _context.Equipments.ToListAsync();
            return equipments.Select(e => new EquipmentDto{
                Id = e.Id,
                Name = e.Name,
                Description = e.Description
            });
        }

        public async Task<EquipmentDto> GetEquipmentByIdAsync(int id)
        {
            var equipment = await _context.Equipments
                .FirstOrDefaultAsync(e => e.Id == id);

            return equipment == null
                ? null
                : new EquipmentDto
                {
                    Id = equipment.Id,
                    Name = equipment.Name,
                    Description = equipment.Description
                };
        }

        public async Task<EquipmentDto> CreateEquipmentAsync(CreateEquipmentDto createEquipmentDto)
        {
            var equipment = new Equipment
            {
                Name = createEquipmentDto.Name,
                Description = createEquipmentDto.Description,
                // Map other properties if needed
            };

            await _context.Equipments.AddAsync(equipment);
            await SaveChangesAsync();

            // Return the created equipment as EquipmentDto
            return new EquipmentDto
            {
                Id = equipment.Id,
                Name = equipment.Name,
                Description = equipment.Description,
                // Map other properties if needed
            };
        }


          public async Task<bool> CheckEquipmentExistsByNameAsync(string name)
    {
        return await _context.Equipments.AnyAsync(e => e.Name == name);
    }
    }
}