using System.ComponentModel.DataAnnotations;

namespace asteelincident.Server.Models
{
    public class IncidentLog
    {

        [Key]
        public int LogID { get; set; }
        public int IncidentID { get; set; }
        public string Action { get; set; }
        public int ActionBy { get; set; }
        public DateTime ActionDate { get; set; }

        // Navigation properties
        public Incident Incident { get; set; }
        public User ActionByUser { get; set; }
    }

}
