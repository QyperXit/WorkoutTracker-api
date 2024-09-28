using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WorkoutTracker_api.Dto;
using WorkoutTracker_api.Interfaces;

namespace WorkoutTracker_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExerciseController  : ControllerBase
    {
        private readonly IExerciseRepository _exerciseRepository;
        public ExerciseController (IExerciseRepository exerciseRepository)
        {
            _exerciseRepository = exerciseRepository;
            
        }


        [HttpGet]
           public async Task<ActionResult<IEnumerable<ExerciseDto>>> GetAllExercisesAsync()
        {
            var exercises = await _exerciseRepository.GetAllExercisesAsync(); 
            return Ok(exercises);
        }

            [HttpGet("{id}")]
        public async Task<ActionResult<ExerciseDto>> GetExerciseById(int id)
        {
            var exercise = await _exerciseRepository.GetExerciseByIdAsync(id);
            if (exercise == null)
            {
                return NotFound();
            }
            return Ok(exercise);
        }

           [HttpPost]
        public async Task<ActionResult<ExerciseDto>> CreateExercise(CreateExerciseDto createExerciseDto)
        {
            if (createExerciseDto == null) 
                return BadRequest("Exercise data is required.");

            if (await _exerciseRepository.CheckExerciseExistsByNameAsync(createExerciseDto.Name)) 
            {
                return BadRequest("Exercise with this name already exists.");
            }

            var createdExercise = await _exerciseRepository.CreateExerciseAsync(createExerciseDto);

            // Check if createdExercise is valid
            if (createdExercise == null || createdExercise.Id <= 0)
            {
                return BadRequest("Error creating exercise.");
            }

            return CreatedAtAction(nameof(GetExerciseById), new { id = createdExercise.Id }, createdExercise);  }

            [HttpDelete("{id}")]
            public async Task<ActionResult> DeleteExercise(int id)
            {
                if (!await _exerciseRepository.CheckExerciseExistsAsync(id))
                {
                    return NotFound(); 
                }

                // Proceed to delete
                var deleted = await _exerciseRepository.DeleteExerciseAsync(id);
                if (!deleted)
                {
                    return BadRequest("Error deleting exercise."); 
                }

                return NoContent(); 
            }

            [HttpPut("{id}")]
            public async Task<ActionResult> UpdateExercise(int id, UpdateExerciseDto updateExerciseDto)
            {
                if (!await _exerciseRepository.CheckExerciseExistsAsync(id))
                {
                    return NotFound();
                }

                var success = await _exerciseRepository.UpdateExerciseAsync(id, updateExerciseDto);
                if (!success)
                {
                    return BadRequest("Failed to update exercise.");
                }

                return NoContent(); 
            }
    }
}