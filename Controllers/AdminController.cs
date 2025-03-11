using EventManagementWithAuthentication.Models;
using EventManagementWithAuthentication.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EventManagementWithAuthentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IEventRepository _eventRepository;

        public AdminController(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        [HttpPost("launchEvent")]
        public async Task<IActionResult> LaunchEvent([FromBody] Event eventModel)
        {
            if (eventModel == null)
            {
                return BadRequest("Event details are required.");
            }

            await _eventRepository.AddEventAsync(eventModel);
            return Ok("Event launched successfully.");
        }

        [HttpGet("getAllEvents")]
        public async Task<IActionResult> GetAllEvents()
        {
            var events = await _eventRepository.GetAllEventsAsync();
            return Ok(events);
        }

        [HttpGet("getEvent/{id}")]
        public async Task<IActionResult> GetEvent(int id)
        {
            var eventEntity = await _eventRepository.GetEventByIdAsync(id);
            if (eventEntity == null)
            {
                return NotFound();
            }
            return Ok(eventEntity);
        }

        [HttpPut("updateEvent/{id}")]
        public async Task<IActionResult> UpdateEvent(int id, [FromBody] Event updatedEvent)
        {
            if (id != updatedEvent.Id)
            {
                return BadRequest("Event ID mismatch.");
            }

            await _eventRepository.UpdateEventAsync(updatedEvent);
            return Ok("Event updated successfully.");
        }

        [HttpDelete("deleteEvent/{id}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            await _eventRepository.DeleteEventAsync(id);
            return Ok("Event deleted successfully.");
        }
    }
}