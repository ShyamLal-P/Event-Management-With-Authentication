using EventManagementWithAuthentication.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EventManagementWithAuthentication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CancellationController : ControllerBase
    {
        private readonly ICancellationService _cancellationService;

        public CancellationController(ICancellationService cancellationService)
        {
            _cancellationService = cancellationService;
        }

        [HttpPost("cancel")]
        public async Task<IActionResult> CancelTickets(int userId, int eventId, int numberOfTickets)
        {
            try
            {
                await _cancellationService.CancelTicketsAsync(userId, eventId, numberOfTickets);
                return Ok(new { Message = "Tickets cancelled successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}
