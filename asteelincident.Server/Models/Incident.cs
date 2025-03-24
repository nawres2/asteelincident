
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;


namespace asteelincident.Server.Models
{
    public class Incident
    {

        [Key]
        public int IncidentID { get; set; }

        [Required]
        public int IncidentTypeID { get; set; }
        [Required(ErrorMessage = "Le titre est obligatoire")]

        public string Title { get; set; }
        [Required(ErrorMessage = "La description est obligatoire")]

        public string Description { get; set; }
        public int CreatedBy { get; set; }
        public int? AssignedTo { get; set; }
        public string Status { get; set; }

        [Required]
        public int PriorityID { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Navigation properties
        public IncidentType IncidentType { get; set; }
        public Users CreatedByUser { get; set; }
        public Users AssignedToUser { get; set; }
        public Priority Priority { get; set; }
        public ICollection<IncidentComment> Comments { get; set; }
        public ICollection<IncidentLog> Logs { get; set; }
        public ICollection<Attachment> Attachments { get; set; }
        public ICollection<Notification> Notifications { get; set; }
        public Report Report { get; set; }
    }

}
