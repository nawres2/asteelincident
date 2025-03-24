using System.ComponentModel.DataAnnotations;

namespace asteelincident.Server.Models
{
    public class Role
    {

        [Key]
        public int RoleID { get; set; }
        public string RoleName { get; set; }

        // Navigation property
        public ICollection<User> Users { get; set; }
    }
}
