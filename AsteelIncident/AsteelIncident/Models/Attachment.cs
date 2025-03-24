namespace AsteelIncident.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Attachment
    {
        public int AttachmentID { get; set; }

        public int IncidentID { get; set; }

        [Required]
        [StringLength(255)]
        public string FilePath { get; set; }

        public int UploadedBy { get; set; }

        public DateTime? UploadedAt { get; set; }

        public virtual Incident Incident { get; set; }

        public virtual User User { get; set; }
    }
}
