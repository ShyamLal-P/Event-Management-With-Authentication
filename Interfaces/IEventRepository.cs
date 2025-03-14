using EventManagementWithAuthentication.Models;

namespace EventManagementWithAuthentication.Interfaces
{
    public interface IEventRepository
    {
        Task<IEnumerable<Event>> GetAllEventsAsync();
        Task<IEnumerable<Event>> GetEventsByUserIdAsync(int userId);
        Task<Event> GetEventByIdAsync(int id);
        Task AddEventAsync(Event evententity);
        Task UpdateEventAsync(Event eventEntity);
        Task DeleteEventAsync(int id);
        void AddEvent(Event newEvent);
    }
}
