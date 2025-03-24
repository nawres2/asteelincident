using System.ComponentModel.DataAnnotations;

namespace asteelincident.Server.Models
{
    public class Attachment
    {
        [Key]
        public int AttachmentID { get; set; }
        public int IncidentID { get; set; }
        public string FilePath { get; set; }
        public int UploadedBy { get; set; }
        public DateTime UploadedAt { get; set; }

        // Navigation properties
        public Incident Incident { get; set; }
        public User UploadedByUser { get; set; }
    }

}
