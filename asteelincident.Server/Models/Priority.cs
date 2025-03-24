using System.ComponentModel.DataAnnotations;

namespace asteelincident.Server.Models
{
    public class Priority
    {

        [Key]
        public int PriorityID { get; set; }
        public string PriorityName { get; set; }
        public int SLA_Days { get; set; }

        // Navigation property
        public ICollection<Incident> Incidents { get; set; }
    }
}
