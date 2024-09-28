using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WorkoutTracker_api.Dto;
using WorkoutTracker_api.Interfaces;
using WorkoutTracker_api.Models;

namespace WorkoutTracker_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExerciseEquipmentController : ControllerBase
    {
        private readonly IExerciseEquipmentRepository _exerciseEquipmentRepository;

        public ExerciseEquipmentController(IExerciseEquipmentRepository exerciseEquipmentRepository)   
        {
            _exerciseEquipmentRepository = exerciseEquipmentRepository;
        }

        // GET: api/ExerciseEquipment
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ExerciseEquipment>))]
           public async Task<IActionResult> GetAllExerciseEquipment()
        {
            var exerciseEquipment = await _exerciseEquipmentRepository.GetAllAsync();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(exerciseEquipment);
        }

        // get

        [HttpGet("{exerciseId}/{equipmentId}")]
        public async Task<IActionResult> GetExerciseEquipment(int exerciseId, int equipmentId)
            {
                var exerciseEquipment = await _exerciseEquipmentRepository.GetByIdsAsync(exerciseId, equipmentId);

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (exerciseEquipment == null)
                    return NotFound();

                return Ok(exerciseEquipment);
            }

            [HttpPost]
        public async Task<ActionResult<ExerciseEquipmentDto>> CreateExerciseEquipment([FromBody] CreateExerciseEquipmentDto dto)
        {
            if (dto == null)
            {
                return BadRequest("Invalid exercise-equipment data.");
            }

            var createdExerciseEquipment = await _exerciseEquipmentRepository.CreateExerciseEquipmentAsync(dto);

            if (createdExerciseEquipment == null)
            {
                return Conflict("The relationship between exercise and equipment already exists.");
            }

            return CreatedAtAction(nameof(GetExerciseEquipment), new { exerciseId = createdExerciseEquipment.ExerciseId, equipmentId = createdExerciseEquipment.EquipmentId }, createdExerciseEquipment);
        }

        [HttpDelete("{exerciseId}/{equipmentId}")]
        public async Task<IActionResult> DeleteExerciseEquipment(int exerciseId, int equipmentId)
        {
            var deleted = await _exerciseEquipmentRepository.DeleteExerciseEquipmentAsync(exerciseId, equipmentId);

            if (!deleted)
            {
                return NotFound("The exercise-equipment link was not found.");
            }

            return NoContent(); // 204 No Content response on successful deletion
        }



      
    }
}