using EventManagementWithAuthentication.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EventManagementWithAuthentication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpPost("book")]
        public async Task<IActionResult> BookTickets(int userId, int eventId, int numberOfTickets)
        {
            try
            {
                var totalCost = await _bookingService.BookTicketsAsync(userId, eventId, numberOfTickets);
                return Ok(new { TotalCost = totalCost });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}
