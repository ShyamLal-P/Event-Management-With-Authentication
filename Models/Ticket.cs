namespace EventManagementWithAuthentication.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        // Foreign Key
        public int EventId { get; set; }
        public virtual Event? Event { get; set; }
        // Foreign Key
        public int UserId { get; set; }
        public virtual User? User { get; set; }
        public DateOnly BookingDate { get; set; }
        public string Status { get; set; }
    }
}
