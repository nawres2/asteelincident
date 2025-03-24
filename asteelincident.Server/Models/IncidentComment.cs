using System.ComponentModel.DataAnnotations;

namespace asteelincident.Server.Models
{
    public class IncidentComment
    {

        [Key]
        public int CommentID { get; set; }
        public int IncidentID { get; set; }
        public string Comment { get; set; }
        public int CommentedBy { get; set; }
        public DateTime CommentedAt { get; set; }

        // Navigation properties
        public Incident Incident { get; set; }
        public User CommentedByUser { get; set; }
    }

}
