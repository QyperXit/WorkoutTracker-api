
using Microsoft.EntityFrameworkCore;
using WorkoutTracker_api.Models;
using WorkoutTracker_api.Interfaces;
using WorkoutTracker_api.DBContext;
using WorkoutTracker_api.Dto;
using WorkoutTracker_api.Mappers;

namespace WorkoutTracker_api.Repository
{
    public class ExerciseEquipmentRepository : IExerciseEquipmentRepository
    {


    
        private readonly DataContext _context;

        public ExerciseEquipmentRepository(DataContext _context)
        {
                this._context = _context;
                //  _context = _context;

        
        }

        public IEnumerable<ExerciseEquipmentDto> GetAll()
        {
   
        return _context.ExerciseEquipments
        .Include(ee => ee.Exercise)
        .Include(ee => ee.Equipment)
        .Select(ee => ExerciseEquipmentMapper.ToDto(
            ee,
            ee.Exercise.Name, // Eager loaded Exercise name
            ee.Equipment.Name // Eager loaded Equipment name
        ))
        .ToList();

        }

            public ExerciseEquipmentDto GetByIds(int exerciseId, int equipmentId)
        {
        var exerciseEquipment = _context.ExerciseEquipments
                .Include(ee => ee.Exercise)   // Eager load the related Exercise entity
                .Include(ee => ee.Equipment)   // Eager load the related Equipment entity
                .FirstOrDefault(ee => ee.ExerciseId == exerciseId && ee.EquipmentId == equipmentId);

            if (exerciseEquipment == null)
            {
                return null;  // Or handle this as needed (e.g., throw an exception or return a default value)
            }

            // Map to ExerciseEquipmentDto using the ToDto method
            return ExerciseEquipmentMapper.ToDto(
                exerciseEquipment,
                exerciseEquipment.Exercise.Name, // Eager loaded Exercise name
                exerciseEquipment.Equipment.Name  // Eager loaded Equipment name
            );
        }

                public async Task<ExerciseEquipmentDto> CreateExerciseEquipmentAsync(CreateExerciseEquipmentDto dto)
        {
            // Ensure the relationship doesn't already exist
            var existing = await _context.ExerciseEquipments
                .FirstOrDefaultAsync(ee => ee.ExerciseId == dto.ExerciseId && ee.EquipmentId == dto.EquipmentId);

            if (existing != null) 
            {
                return null; // or handle accordingly if the relationship already exists
            }

            var exerciseEquipment = new ExerciseEquipment
            {
                ExerciseId = dto.ExerciseId,
                EquipmentId = dto.EquipmentId
            };

            await _context.ExerciseEquipments.AddAsync(exerciseEquipment);
            await _context.SaveChangesAsync();

            // Optionally load the exercise and equipment names for the response
            var created = await _context.ExerciseEquipments
                .Include(ee => ee.Exercise)
                .Include(ee => ee.Equipment)
                .FirstOrDefaultAsync(ee => ee.ExerciseId == exerciseEquipment.ExerciseId && ee.EquipmentId == exerciseEquipment.EquipmentId);

            return ExerciseEquipmentMapper.ToDto(created, created.Exercise.Name, created.Equipment.Name);
        }

            public async Task<bool> DeleteExerciseEquipmentAsync(int exerciseId, int equipmentId)
        {
            var exerciseEquipment = await _context.ExerciseEquipments
                .FirstOrDefaultAsync(ee => ee.ExerciseId == exerciseId && ee.EquipmentId == equipmentId);

            if (exerciseEquipment == null) return false;

            _context.ExerciseEquipments.Remove(exerciseEquipment);
            await _context.SaveChangesAsync();
            return true;
        }



}
}