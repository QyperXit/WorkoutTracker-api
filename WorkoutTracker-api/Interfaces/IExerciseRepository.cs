using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkoutTracker_api.Dto;

namespace WorkoutTracker_api.Interfaces
{
    public interface IExerciseRepository
    {
        Task<IEnumerable<ExerciseDto>> GetAllExercisesAsync();
        Task<ExerciseDto> GetExerciseByIdAsync(int id);
        Task<ExerciseDto> CreateExerciseAsync(CreateExerciseDto createExerciseDto);
        Task<bool> CheckExerciseExistsByNameAsync(string name);
        Task<bool> CheckExerciseExistsAsync(int id);
        Task<bool> SaveChangesAsync();

       
    }
}