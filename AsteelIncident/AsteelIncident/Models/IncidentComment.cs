namespace AsteelIncident.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class IncidentComment
    {
        [Key]
        public int CommentID { get; set; }

        public int IncidentID { get; set; }

        [Required]
        public string Comment { get; set; }

        public int CommentedBy { get; set; }

        public DateTime? CommentedAt { get; set; }

        public virtual User User { get; set; }

        public virtual Incident Incident { get; set; }
    }
}
