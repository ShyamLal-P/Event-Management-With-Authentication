using EventManagementWithAuthentication.Interfaces;
using EventManagementWithAuthentication.Models;

namespace EventManagementWithAuthentication.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IEventRepository _eventRepository;
        private readonly INotificationRepository _notificationRepository;

        public NotificationService(IEventRepository eventRepository, INotificationRepository notificationRepository)
        {
            _eventRepository = eventRepository;
            _notificationRepository = notificationRepository;
        }

        public async Task<List<Notification>> GetNotificationsForUserAsync(int userId)
        {
            var notifications = await _notificationRepository.GetNotificationsByUserIdAsync(userId);
            var bookedNotifications = new List<Notification>();

            var groupedNotifications = notifications
                .Where(n => n.Ticket != null && n.Ticket.Status == "Booked")
                .GroupBy(n => n.EventId)
                .Select(g => g.First())
                .ToList();

            foreach (var notification in groupedNotifications)
            {
                var eventItem = await _eventRepository.GetEventByIdAsync(notification.EventId);
                notification.Message = $"Ticket booked! Time left for the event: {CalculateTimeLeft(eventItem)}";
                bookedNotifications.Add(notification);
            }

            return bookedNotifications;
        }

        private string CalculateTimeLeft(Event eventItem)
        {
            DateTime eventDateTime = new DateTime(eventItem.Date.Year, eventItem.Date.Month, eventItem.Date.Day, eventItem.Time.Hour, eventItem.Time.Minute, eventItem.Time.Second);
            DateTime currentDateTime = DateTime.Now;

            if (eventDateTime < currentDateTime)
            {
                return "Event has already passed.";
            }

            TimeSpan timeLeft = eventDateTime - currentDateTime;
            return $"{timeLeft.Days} days, {timeLeft.Hours} hours, {timeLeft.Minutes} minutes.";
        }
    }
}
