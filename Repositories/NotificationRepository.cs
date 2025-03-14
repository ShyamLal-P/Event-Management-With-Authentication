using EventManagementWithAuthentication.Data;
using EventManagementWithAuthentication.Interfaces;
using EventManagementWithAuthentication.Models;
using Microsoft.EntityFrameworkCore;

namespace EventManagementWithAuthentication.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly ApplicationDbContext _context;

        public NotificationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Notification>> GetAllNotificationsAsync()
        {
            return await _context.Notifications.ToListAsync();
        }

        public async Task<Notification> GetNotificationByIdAsync(int id)
        {
            return await _context.Notifications.FindAsync(id);
        }

        public async Task AddNotificationAsync(Notification notification)
        {
            await _context.Notifications.AddAsync(notification);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateNotificationAsync(Notification notification)
        {
            _context.Notifications.Update(notification);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteNotificationAsync(int id)
        {
            var notification = await _context.Notifications.FindAsync(id);
            if (notification != null)
            {
                _context.Notifications.Remove(notification);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Notification>> GetNotificationsByUserIdAsync(int userId)
        {
            return await _context.Notifications
                .Where(n => n.UserId == userId)
                .Include(n => n.Event)
                .Include(n => n.Ticket)
                .ToListAsync();
        }
    } 
}
