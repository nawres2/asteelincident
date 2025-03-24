using System.ComponentModel.DataAnnotations;

namespace asteelincident.Server.Models
{
    public class Users
    {

        [Key]
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int DepartmentID { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Department Department { get; set; }

        public int RoleID { get; set; }
        public Role Role { get; set; }
    }
}
