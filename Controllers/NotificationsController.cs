using EventManagementWithAuthentication.Models;
using EventManagementWithAuthentication.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EventManagementWithAuthentication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationsController : ControllerBase
    {
        private readonly INotificationRepository _notificationRepository;

        public NotificationsController(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllNotifications()
        {
            var notifications = await _notificationRepository.GetAllNotificationsAsync();
            return Ok(notifications);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetNotificationById(int id)
        {
            var notification = await _notificationRepository.GetNotificationByIdAsync(id);
            if (notification == null)
            {
                return NotFound();
            }
            return Ok(notification);
        }

        [HttpPost]
        public async Task<IActionResult> AddNotification(Notification notification)
        {
            await _notificationRepository.AddNotificationAsync(notification);
            return CreatedAtAction(nameof(GetNotificationById), new { id = notification.Id }, notification);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNotification(int id, Notification notification)
        {
            if (id != notification.Id)
            {
                return BadRequest();
            }
            await _notificationRepository.UpdateNotificationAsync(notification);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNotification(int id)
        {
            await _notificationRepository.DeleteNotificationAsync(id);
            return NoContent();
        }
    }
}
