
using Microsoft.EntityFrameworkCore;
using WorkoutTracker_api.Models;
using WorkoutTracker_api.Interfaces;
using WorkoutTracker_api.DBContext;
using WorkoutTracker_api.Dto;

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
        .Select(ee => new ExerciseEquipmentDto
        {
            ExerciseId = ee.ExerciseId,
            ExerciseName = ee.Exercise.Name, 
            EquipmentId = ee.EquipmentId,
            EquipmentName = ee.Equipment.Name 
        })
        .ToList();

        }
    }
}