namespace AsteelIncident.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Incident
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Incident()
        {
            Attachments = new HashSet<Attachment>();
            IncidentComments = new HashSet<IncidentComment>();
            IncidentLogs = new HashSet<IncidentLog>();
            Notifications = new HashSet<Notification>();
            Reports = new HashSet<Report>();
        }

        public int IncidentID { get; set; }

        public int IncidentTypeID { get; set; }

        [Required]
        [StringLength(255)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public int CreatedBy { get; set; }

        public int? AssignedTo { get; set; }

        [StringLength(50)]
        public string Status { get; set; }

        public int? PriorityID { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Attachment> Attachments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<IncidentComment> IncidentComments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<IncidentLog> IncidentLogs { get; set; }

        public virtual User User { get; set; }

        public virtual User User1 { get; set; }

        public virtual IncidentType IncidentType { get; set; }

        public virtual Priority Priority { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Notification> Notifications { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Report> Reports { get; set; }
    }
}
