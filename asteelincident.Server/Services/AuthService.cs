using asteelincident.Server.Data;
using asteelincident.Server.Models;

namespace asteelincident.Server.Services
{
    public class AuthService
    {

        private readonly ApplicationDbContext _context;

        public AuthService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Users Authenticate(string username, string password)  // ✅ Utilisez "User" et non "Users"
        {
            return _context.Users.FirstOrDefault(u => u.Username == username && u.Password == password);
        }
    }
}
