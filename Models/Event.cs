namespace EventManagementWithAuthentication.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Location { get; set; }
        public DateOnly Date { get; set; }
        public int NoOfTickets { get; set; }
        // Foreign Key
        public int OrganizerId { get; set; }

        // Navigation Property
        public virtual User? User { get; set; }
        public virtual ICollection<Ticket>? Tickets { get; set; }
        public virtual ICollection<Notification>? Notifications { get; set; }
        public virtual ICollection<Feedback>? Feedbacks { get; set; }

    }
}
