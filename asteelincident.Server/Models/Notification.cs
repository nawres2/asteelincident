using System.ComponentModel.DataAnnotations;

namespace asteelincident.Server.Models
{
    public class Notification
    {


        [Key]
        public int NotificationID { get; set; }
        public int UserID { get; set; }
        public int? IncidentID { get; set; }
        public string Message { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }

        // Navigation properties
        public User User { get; set; }
        public Incident Incident { get; set; }
    }

}
