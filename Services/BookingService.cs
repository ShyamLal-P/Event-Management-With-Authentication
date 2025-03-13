using EventManagementWithAuthentication.Models;
using EventManagementWithAuthentication.Repositories.Interfaces;

namespace EventManagementWithAuthentication.Services
{
    public class BookingService : IBookingService
    {
        private readonly IEventRepository _eventRepository;
        private readonly ITicketRepository _ticketRepository;
        private readonly IUserRepository _userRepository;

        public BookingService(IEventRepository eventRepository, ITicketRepository ticketRepository, IUserRepository userRepository)
        {
            _eventRepository = eventRepository;
            _ticketRepository = ticketRepository;
            _userRepository = userRepository;
        }

        public async Task<double> BookTicketsAsync(int userId, int eventId, int numberOfTickets)
        {
            var eventDetails = await _eventRepository.GetEventByIdAsync(eventId);
            if (eventDetails == null)
            {
                throw new Exception("Event not found.");
            }

            if (eventDetails.NoOfTickets < numberOfTickets)
            {
                throw new Exception("Not enough tickets available.");
            }

            var totalCost = numberOfTickets * eventDetails.EventPrice;

            for (int i = 0; i < numberOfTickets; i++)
            {
                var ticket = new Ticket
                {
                    EventId = eventId,
                    UserId = userId,
                    BookingDate = DateOnly.FromDateTime(DateTime.Now),
                    Status = "Booked"
                };
                await _ticketRepository.AddTicketAsync(ticket);
            }

            eventDetails.NoOfTickets -= numberOfTickets;
            await _eventRepository.UpdateEventAsync(eventDetails);

            return totalCost;
        }
    }
}
