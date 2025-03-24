namespace AsteelIncident.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Report
    {
        public int ReportID { get; set; }

        public int IncidentID { get; set; }

        public string ResolutionDetails { get; set; }

        public int? ResolvedBy { get; set; }

        public DateTime? ResolvedAt { get; set; }

        public virtual Incident Incident { get; set; }

        public virtual User User { get; set; }
    }
}
