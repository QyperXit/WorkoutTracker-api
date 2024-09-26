using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WorkoutTracker_api.DBContext;
using WorkoutTracker_api.Dto;
using WorkoutTracker_api.Interfaces;

namespace WorkoutTracker_api.Repository
{
    public class EquipmentRepository : IEquipmentRepository
    {
        private readonly DataContext _context;
        public EquipmentRepository(DataContext _context)
        {
            this._context = _context;
            
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
    }
}