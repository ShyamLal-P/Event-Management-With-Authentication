namespace EventManagementWithAuthentication.Interfaces
{
    public interface ICancellationService
    {
        Task CancelTicketsAsync(int userId, int eventId, int numberOfTickets);
    }
}
