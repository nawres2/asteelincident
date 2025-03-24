using System.ComponentModel.DataAnnotations;

namespace asteelincident.Server.Models
{
    public class Report
    {

        [Key]
        public int ReportID { get; set; }
        public int IncidentID { get; set; }
        public string ResolutionDetails { get; set; }
        public int? ResolvedBy { get; set; }
        public DateTime? ResolvedAt { get; set; }

        // Navigation properties
        public Incident Incident { get; set; }
        public User ResolvedByUser { get; set; }
    }

}
