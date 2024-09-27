using WorkoutTracker_api.DBContext.Dto;
using WorkoutTracker_api.Dto;
using WorkoutTracker_api.Models;


namespace WorkoutTracker_api.Interfaces
{
    public interface IExerciseEquipmentRepository
    {
        IEnumerable<ExerciseEquipmentDto> GetAll();
        ExerciseEquipmentDto GetByIds(int exerciseId, int equipmentId);
        // bool Add(ExerciseEquipmentDto exerciseEquipmentDto);
        // bool Delete(int exerciseId, int equipmentId);
        // bool Exists(int exerciseId, int equipmentId);
        // bool SaveChanges();
    }
}