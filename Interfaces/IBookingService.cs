namespace EventManagementWithAuthentication.Interfaces
{
    public interface IBookingService
    {
        Task<string> BookTicketsAsync(int userId, int eventId, int numberOfTickets);

    }
}
