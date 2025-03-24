namespace AsteelIncident.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Equipment
    {
        public int EquipmentID { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        public string Type { get; set; }

        [Required]
        [StringLength(50)]
        public string SerialNumber { get; set; }

        [StringLength(100)]
        public string Location { get; set; }

        public int? AssignedTo { get; set; }

        [Column(TypeName = "date")]
        public DateTime? MaintenanceDate { get; set; }

        public virtual User User { get; set; }
    }
}
