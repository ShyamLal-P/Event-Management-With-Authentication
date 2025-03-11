using Microsoft.Extensions.Logging;
using System.Net.Sockets;

namespace EventManagementWithAuthentication.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public long ContactNumber { get; set; }
        public string Role { get; set; }

        public virtual ICollection<Event>? Events { get; set; }
        public virtual ICollection<Ticket>? Tickets { get; set; }
        public virtual ICollection<Notification>? Notifications { get; set; }
        public virtual ICollection<Feedback>? Feedbacks { get; set; }
    }
}
