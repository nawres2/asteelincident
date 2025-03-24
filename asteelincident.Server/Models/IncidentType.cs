using System.ComponentModel.DataAnnotations;

namespace asteelincident.Server.Models
{
    public class IncidentType
    {

        [Key]
        public int TypeID { get; set; }
        public string TypeName { get; set; }
        public string Description { get; set; }

        // Navigation property
        public ICollection<Incident> Incidents { get; set; }
    }
}
