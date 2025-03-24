using System.ComponentModel.DataAnnotations;

namespace asteelincident.Server.Models
{
    public class Department
    {


        [Key]

        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }

        // Navigation property
        public ICollection<User> Users { get; set; }
    }
}
