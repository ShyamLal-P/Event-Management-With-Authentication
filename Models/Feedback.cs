namespace EventManagementWithAuthentication.Models
{
    public class Feedback
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public virtual User? User { get; set; }
        public int EventId { get; set; }
        public virtual Event? Event { get; set; }
        public int Rating { get; set; }
        public string Comments { get; set; }
        public TimeOnly SubmittedTimestamp { get; set; }
    }
}
