using EventManagementWithAuthentication.Interfaces;
using EventManagementWithAuthentication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventManagementWithAuthentication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UpcomingEventsController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public UpcomingEventsController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpGet("{userId}")]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> GetUpcomingEvents(int userId)
        {
            var notifications = await _notificationService.GetNotificationsForUserAsync(userId);
            if (notifications == null || !notifications.Any())
            {
                return NotFound(new { Message = "No upcoming events found for this user." });
            }

            return Ok(notifications);
        }
    }
}
