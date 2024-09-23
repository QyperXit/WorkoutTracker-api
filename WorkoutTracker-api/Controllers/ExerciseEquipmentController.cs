using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        public IActionResult GetAllExerciseEquipment()
        {
            var exerciseEquipment = _exerciseEquipmentRepository.GetAll();

            if (!ModelState.IsValid)
            return BadRequest(ModelState);
            

            return Ok(exerciseEquipment);
        }
      
    }
}