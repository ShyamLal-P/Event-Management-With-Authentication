using EventManagementWithAuthentication.Interfaces;
using EventManagementWithAuthentication.Models;

namespace EventManagementWithAuthentication.Services
{
    public class CancellationService : ICancellationService
    {
        private readonly IEventRepository _eventRepository;
        private readonly ITicketRepository _ticketRepository;
        private readonly IUserRepository _userRepository;
        private readonly INotificationRepository _notificationRepository;

        public CancellationService(IEventRepository eventRepository, ITicketRepository ticketRepository, IUserRepository userRepository, INotificationRepository notificationRepository)
        {
            _eventRepository = eventRepository;
            _ticketRepository = ticketRepository;
            _userRepository = userRepository;
            _notificationRepository = notificationRepository;
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

                // Create a cancellation notification
                var notification = new Notification
                {
                    UserId = userId,
                    EventId = eventId,
                    TicketId = ticket.Id,
                    Message = "Ticket cancelled.",
                    SentTime = TimeOnly.FromDateTime(DateTime.Now)
                };
                await _notificationRepository.AddNotificationAsync(notification);
            }

            eventDetails.NoOfTickets += numberOfTickets;
            await _eventRepository.UpdateEventAsync(eventDetails);

            // Additional business logic: Process refund
            await ProcessRefundAsync(userId, numberOfTickets * eventDetails.EventPrice);
        }

        private async Task ProcessRefundAsync(int userId, double amount)
        {
            // Implement refund processing logic here
            // For example, update user's account balance or initiate a refund transaction
        }
    }
}
