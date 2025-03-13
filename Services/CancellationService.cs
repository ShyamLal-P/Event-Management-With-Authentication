using EventManagementWithAuthentication.Repositories.Interfaces;

namespace EventManagementWithAuthentication.Services
{
    public class CancellationService : ICancellationService
    {
        private readonly IEventRepository _eventRepository;
        private readonly ITicketRepository _ticketRepository;
        private readonly IUserRepository _userRepository;

        public CancellationService(IEventRepository eventRepository, ITicketRepository ticketRepository, IUserRepository userRepository)
        {
            _eventRepository = eventRepository;
            _ticketRepository = ticketRepository;
            _userRepository = userRepository;
        }

        public async Task CancelTicketsAsync(int userId, int eventId, int numberOfTickets)
        {
            var eventDetails = await _eventRepository.GetEventByIdAsync(eventId);
            if (eventDetails == null)
            {
                throw new Exception("Event not found.");
            }

            var tickets = await _ticketRepository.GetAllTicketsAsync();
            var userTickets = tickets.Where(t => t.EventId == eventId && t.UserId == userId && t.Status == "Booked").Take(numberOfTickets).ToList();

            if (userTickets.Count < numberOfTickets)
            {
                throw new Exception("Not enough tickets to cancel.");
            }

            foreach (var ticket in userTickets)
            {
                ticket.Status = "Cancelled";
                await _ticketRepository.UpdateTicketAsync(ticket);
            }

            eventDetails.NoOfTickets += numberOfTickets;
            await _eventRepository.UpdateEventAsync(eventDetails);

            // Additional business logic: Process refund and send notification
            await ProcessRefundAsync(userId, numberOfTickets * eventDetails.EventPrice);
            await SendCancellationNotificationAsync(userId, eventId, numberOfTickets);
        }

        private async Task ProcessRefundAsync(int userId, double amount)
        {
            // Implement refund processing logic here
            // For example, update user's account balance or initiate a refund transaction
        }

        private async Task SendCancellationNotificationAsync(int userId, int eventId, int numberOfTickets)
        {
            // Implement notification sending logic here
            // For example, send an email or push notification to the user
        }
    }
}
