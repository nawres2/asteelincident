namespace AsteelIncident.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Notification
    {
        public int NotificationID { get; set; }

        public int UserID { get; set; }

        public int? IncidentID { get; set; }

        [Required]
        [StringLength(255)]
        public string Message { get; set; }

        [StringLength(50)]
        public string Status { get; set; }

        public DateTime? CreatedAt { get; set; }

        public virtual Incident Incident { get; set; }

        public virtual User User { get; set; }
    }
}
