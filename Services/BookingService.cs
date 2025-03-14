using EventManagementWithAuthentication.Interfaces;
using EventManagementWithAuthentication.Models;

namespace EventManagementWithAuthentication.Services
{
    public class BookingService : IBookingService
    {
        private readonly IEventRepository _eventRepository;
        private readonly ITicketRepository _ticketRepository;
        private readonly IUserRepository _userRepository;
        private readonly INotificationRepository _notificationRepository;

        private const int MaxTicketsPerBooking = 6;
        private const double PriceIncreaseThreshold = 0.1; // 10%

        public BookingService(IEventRepository eventRepository, ITicketRepository ticketRepository, IUserRepository userRepository, INotificationRepository notificationRepository)
        {
            _eventRepository = eventRepository;
            _ticketRepository = ticketRepository;
            _userRepository = userRepository;
            _notificationRepository = notificationRepository;
        }

        public async Task<string> BookTicketsAsync(int userId, int eventId, int numberOfTickets)
        {
            if (numberOfTickets > MaxTicketsPerBooking)
            {
                throw new Exception($"You can only book a maximum of {MaxTicketsPerBooking} tickets at once.");
            }

            var eventDetails = await _eventRepository.GetEventByIdAsync(eventId);
            if (eventDetails == null)
            {
                throw new Exception("Event not found.");
            }

            if (eventDetails.NoOfTickets < numberOfTickets)
            {
                throw new Exception($"Not enough tickets available. Only {eventDetails.NoOfTickets} tickets are left.");
            }

            double ticketPrice = eventDetails.EventPrice;
            int totalTickets = eventDetails.NoOfTickets + numberOfTickets; // Calculate total tickets initially available
            if (eventDetails.NoOfTickets <= totalTickets * PriceIncreaseThreshold)
            {
                ticketPrice *= 1.1; // Increase price by 10%
            }

            var totalCost = numberOfTickets * ticketPrice;

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

                // Create a notification
                string timeLeft = CalculateTimeLeft(eventDetails);
                var notification = new Notification
                {
                    UserId = userId,
                    EventId = eventId,
                    TicketId = ticket.Id,
                    Message = $"Ticket booked! Time left for the event: {timeLeft}",
                    SentTime = TimeOnly.FromDateTime(DateTime.Now)
                };
                await _notificationRepository.AddNotificationAsync(notification);
            }

            eventDetails.NoOfTickets -= numberOfTickets;
            await _eventRepository.UpdateEventAsync(eventDetails);

            // Generate bill
            string bill = GenerateBill(numberOfTickets, ticketPrice, totalCost);

            return bill;
        }

        private string CalculateTimeLeft(Event eventDetails)
        {
            DateTime eventDateTime = new DateTime(eventDetails.Date.Year, eventDetails.Date.Month, eventDetails.Date.Day, eventDetails.Time.Hour, eventDetails.Time.Minute, eventDetails.Time.Second);
            DateTime currentDateTime = DateTime.Now;

            if (eventDateTime < currentDateTime)
            {
                return "Event has already passed.";
            }

            TimeSpan timeLeft = eventDateTime - currentDateTime;
            return $"{timeLeft.Days} days, {timeLeft.Hours} hours, {timeLeft.Minutes} minutes.";
        }

        private string GenerateBill(int numberOfTickets, double ticketPrice, double totalCost)
        {
            return $"Total tickets: {numberOfTickets}\n" +
                   $"Ticket price: {ticketPrice:C}\n" +
                   $"{numberOfTickets} x {ticketPrice:C} = {totalCost:C}\n" +
                   $"Total cost: {totalCost:C}";
        }
    }
}
