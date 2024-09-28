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
    public class ExerciseRepository : IExerciseRepository
    {
        
        private readonly DataContext _context;

        public ExerciseRepository(DataContext _context)
        {
            this._context = _context;
            
        }

        public async Task<IEnumerable<ExerciseDto>> GetAllExercisesAsync()
        {
              var exercises = await _context.Exercises.ToListAsync();
            return exercises.Select(e => new ExerciseDto{
                Id = e.Id,
                Name = e.Name,
                Description = e.Description,
                PrimaryMuscleGroup = e.PrimaryMuscleGroup
                
            });
        }

        public async Task<ExerciseDto> GetExerciseByIdAsync(int id)
        {
             var exercise = await _context.Exercises
                .FirstOrDefaultAsync(e => e.Id == id);

            return exercise == null
                ? null
                : new ExerciseDto
                {
                    Id = exercise.Id,
                    Name = exercise.Name,
                    Description = exercise.Description,
                    PrimaryMuscleGroup = exercise.PrimaryMuscleGroup
                };
        }

                public async Task<ExerciseDto> CreateExerciseAsync(CreateExerciseDto createExerciseDto)
        {
            var exercise = new Exercise
            {
                Name = createExerciseDto.Name,
                Description = createExerciseDto.Description,
                PrimaryMuscleGroup = createExerciseDto.PrimaryMuscleGroup
                
            };

            await _context.Exercises.AddAsync(exercise);
            await SaveChangesAsync();

            return new ExerciseDto
            {
                Id = exercise.Id,
                Name = exercise.Name,
                Description = exercise.Description,
                PrimaryMuscleGroup = exercise.PrimaryMuscleGroup
            };
        }



          public async Task<bool> CheckExerciseExistsByNameAsync(string name)
        {
            return await _context.Exercises.AnyAsync(e => e.Name == name);
        }

          public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0; // Return true if save was successful
        }

            public async Task<bool> CheckExerciseExistsAsync(int id)
        {
            return await _context.Exercises.AnyAsync(e => e.Id == id);
        }

        public async Task<bool> DeleteExerciseAsync(int id)
        {
            var exercise = await _context.Exercises.FindAsync(id);
            if (exercise == null)
                return false; // Return false if not found

            _context.Exercises.Remove(exercise); // Remove the exercise
            return await SaveChangesAsync(); 
        }
    }
}