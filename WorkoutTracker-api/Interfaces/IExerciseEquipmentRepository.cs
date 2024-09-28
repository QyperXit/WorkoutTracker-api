using WorkoutTracker_api.DBContext.Dto;
using WorkoutTracker_api.Dto;
using WorkoutTracker_api.Models;


namespace WorkoutTracker_api.Interfaces
{
    public interface IExerciseEquipmentRepository
    {
       Task<IEnumerable<ExerciseEquipmentDto>> GetAllAsync(); 
        Task<ExerciseEquipmentDto> GetByIdsAsync(int exerciseId, int equipmentId);  
        Task<ExerciseEquipmentDto> CreateExerciseEquipmentAsync(CreateExerciseEquipmentDto dto);
        Task<bool> DeleteExerciseEquipmentAsync(int exerciseId, int equipmentId);
        Task<bool> SaveChangesAsync();

    }
}