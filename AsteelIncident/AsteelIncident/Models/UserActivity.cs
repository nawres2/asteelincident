namespace AsteelIncident.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class UserActivity
    {
        [Key]
        public int ActivityID { get; set; }

        public int UserID { get; set; }

        [Required]
        [StringLength(255)]
        public string Activity { get; set; }

        public DateTime? ActivityDate { get; set; }

        [StringLength(50)]
        public string IPAddress { get; set; }

        public virtual User User { get; set; }
    }
}
