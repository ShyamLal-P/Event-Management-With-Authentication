namespace EventManagementWithAuthentication.Repositories.Interfaces
{
    public interface IBookingService
    {
        Task<double> BookTicketsAsync(int userId, int eventId, int numberOfTickets);
    }
}
