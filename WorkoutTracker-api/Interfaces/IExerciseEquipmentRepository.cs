using WorkoutTracker_api.DBContext.Dto;
using WorkoutTracker_api.Dto;
using WorkoutTracker_api.Models;


namespace WorkoutTracker_api.Interfaces
{
    public interface IExerciseEquipmentRepository
    {
        IEnumerable<ExerciseEquipmentDto> GetAll();
        ExerciseEquipmentDto GetByIds(int exerciseId, int equipmentId);
        Task<ExerciseEquipmentDto> CreateExerciseEquipmentAsync(CreateExerciseEquipmentDto dto);
        Task<bool> DeleteExerciseEquipmentAsync(int exerciseId, int equipmentId);


    
        // bool SaveChanges();
    }
}