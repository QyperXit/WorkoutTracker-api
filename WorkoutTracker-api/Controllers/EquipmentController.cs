using Microsoft.AspNetCore.Mvc;
using WorkoutTracker_api.Dto;
using WorkoutTracker_api.Interfaces;

namespace WorkoutTracker_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipmentController : ControllerBase
    {
        private readonly IEquipmentRepository equipmentRepository;

        public EquipmentController(IEquipmentRepository equipmentRepository)
        {
            this.equipmentRepository = equipmentRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EquipmentDto>>> GetAllEquipments() // Change return type to ActionResult<IEnumerable<EquipmentDto>>
        {
            var equipments = await equipmentRepository.GetAllEquipmentsAsync();
            return Ok(equipments); // Ok() returns ActionResult, which is compatible now
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EquipmentDto>> GetEquipmentById(int id)
        {
            var equipment = await equipmentRepository.GetEquipmentByIdAsync(id);
            if (equipment == null)
            {
                return NotFound();
            }
            return Ok(equipment);
        }

    }
}
