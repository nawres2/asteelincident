using asteelincident.Server.Data;
using asteelincident.Server.Models; // Assure-toi d'inclure le modèle Incident
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;

namespace asteelincident.Server.Services
{
    public class IndexService
    {
        private readonly ApplicationDbContext _context;

        public IndexService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<object?> GetIncidentByIdAsync(int id)
        {
            return await _context.Incidents
                .Where(i => i.IncidentID == id)
                .Select(i => new { title = i.Title, description = i.Description })
                .FirstOrDefaultAsync();
        }
    }
}
