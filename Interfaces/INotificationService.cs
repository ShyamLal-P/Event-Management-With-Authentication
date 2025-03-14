using EventManagementWithAuthentication.Models;

namespace EventManagementWithAuthentication.Interfaces
{
    public interface INotificationService
    {
        Task<List<Notification>> GetNotificationsForUserAsync(int userId);
    }
}
