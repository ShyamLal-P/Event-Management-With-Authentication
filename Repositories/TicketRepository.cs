using EventManagementWithAuthentication.Data;
using EventManagementWithAuthentication.Interfaces;
using EventManagementWithAuthentication.Models;

namespace EventManagementWithAuthentication.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly ApplicationDbContext _context;

        public TicketRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Ticket>> GetAllTicketsAsync()
        {
            return _context.Tickets.ToList();
        }

        public async Task<Ticket> GetTicketByIdAsync(int id)
        {
            return await _context.Tickets.FindAsync(id);
        }

        public async Task AddTicketAsync(Ticket ticket)
        {
            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTicketAsync(Ticket ticket)
        {
            _context.Tickets.Update(ticket);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTicketAsync(int id)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket != null)
            {
                _context.Tickets.Remove(ticket);
                await _context.SaveChangesAsync();
            }
        }
    }
}
