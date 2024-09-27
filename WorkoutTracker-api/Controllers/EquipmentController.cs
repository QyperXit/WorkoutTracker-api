using Microsoft.AspNetCore.Mvc;
using WorkoutTracker_api.Dto;
using WorkoutTracker_api.Interfaces;
using System.Collections.Generic; // Required for IEnumerable<T>
using System.Threading.Tasks;

namespace WorkoutTracker_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipmentController : ControllerBase
    {
        private readonly IEquipmentRepository _equipmentRepository; // Use an underscore for instance variables

        public EquipmentController(IEquipmentRepository equipmentRepository)
        {
            _equipmentRepository = equipmentRepository; // Correctly assign the injected repository
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EquipmentDto>>> GetAllEquipments()
        {
            var equipments = await _equipmentRepository.GetAllEquipmentsAsync(); // Use _equipmentRepository
            return Ok(equipments);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EquipmentDto>> GetEquipmentById(int id)
        {
            var equipment = await _equipmentRepository.GetEquipmentByIdAsync(id);
            if (equipment == null)
            {
                return NotFound();
            }
            return Ok(equipment);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEquipment(int id)
        {
            // Check if the equipment exists
            if (!await _equipmentRepository.CheckEquipmentExistsAsync(id))
            {
                return NotFound(); // Return 404 if not found
            }

            // Proceed to delete
            var deleted = await _equipmentRepository.DeleteEquipmentAsync(id);
            if (!deleted)
            {
                return BadRequest("Error deleting equipment."); // Handle delete error
            }

            return NoContent(); // Return 204 No Content on successful deletion
        }

    }
}
