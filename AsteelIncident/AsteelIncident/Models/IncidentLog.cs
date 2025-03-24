namespace AsteelIncident.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class IncidentLog
    {
        [Key]
        public int LogID { get; set; }

        public int IncidentID { get; set; }

        [Required]
        [StringLength(255)]
        public string Action { get; set; }

        public int ActionBy { get; set; }

        public DateTime? ActionDate { get; set; }

        public virtual User User { get; set; }

        public virtual Incident Incident { get; set; }
    }
}
