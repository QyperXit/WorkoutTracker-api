using Microsoft.AspNetCore.Mvc;
using WorkoutTracker_api.Dto;
using WorkoutTracker_api.Interfaces;
using System.Collections.Generic; 
using System.Threading.Tasks;

namespace WorkoutTracker_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipmentController : ControllerBase
    {
        private readonly IEquipmentRepository _equipmentRepository; 

        public EquipmentController(IEquipmentRepository equipmentRepository)
        {
            _equipmentRepository = equipmentRepository; 
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EquipmentDto>>> GetAllEquipments()
        {
            var equipments = await _equipmentRepository.GetAllEquipmentsAsync(); 
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
                return NotFound(); 
            }

            // Proceed to delete
            var deleted = await _equipmentRepository.DeleteEquipmentAsync(id);
            if (!deleted)
            {
                return BadRequest("Error deleting equipment."); 
            }

            return NoContent(); 
        }

        [HttpPost]
        public async Task<ActionResult<EquipmentDto>> CreateEquipment(CreateEquipmentDto createEquipmentDto)
        {
            if (createEquipmentDto == null) 
                return BadRequest("Equipment data is required.");

            if (await _equipmentRepository.CheckEquipmentExistsByNameAsync(createEquipmentDto.Name)) 
            {
                return BadRequest("Equipment with this name already exists.");
            }

            var createdEquipment = await _equipmentRepository.CreateEquipmentAsync(createEquipmentDto);

            // Check if createdEquipment is valid
            if (createdEquipment == null || createdEquipment.Id <= 0)
            {
                return BadRequest("Error creating equipment.");
            }

            return CreatedAtAction(nameof(GetEquipmentById), new { id = createdEquipment.Id }, createdEquipment);        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateEquipment(int id, UpdateEquipmentDto updateEquipmentDto)
        {
            if (!await _equipmentRepository.CheckEquipmentExistsAsync(id))
            {
                return NotFound();
            }

            var success = await _equipmentRepository.UpdateEquipmentAsync(id, updateEquipmentDto);
            if (!success)
            {
                return BadRequest("Failed to update equipment.");
            }

            return NoContent(); 
        }
    }
}
