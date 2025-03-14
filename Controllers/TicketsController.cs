using EventManagementWithAuthentication.Interfaces;
using EventManagementWithAuthentication.Models;
using Microsoft.AspNetCore.Mvc;

namespace EventManagementWithAuthentication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketsController : ControllerBase
    {
        private readonly ITicketRepository _ticketRepository;

        public TicketsController(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTickets()
        {
            var tickets = await _ticketRepository.GetAllTicketsAsync();
            return Ok(tickets);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTicketById(int id)
        {
            var ticket = await _ticketRepository.GetTicketByIdAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }
            return Ok(ticket);
        }

        [HttpPost]
        public async Task<IActionResult> AddTicket(Ticket ticket)
        {
            await _ticketRepository.AddTicketAsync(ticket);
            return CreatedAtAction(nameof(GetTicketById), new { id = ticket.Id }, ticket);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTicket(int id, Ticket ticket)
        {
            if (id != ticket.Id)
            {
                return BadRequest();
            }
            await _ticketRepository.UpdateTicketAsync(ticket);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicket(int id)
        {
            await _ticketRepository.DeleteTicketAsync(id);
            return NoContent();
        }
    }
}
