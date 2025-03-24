using asteelincident.Server.Data;
using asteelincident.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace asteelincident.Server.Services
{
    public class IncidentService
    {
        private readonly ApplicationDbContext _context;

        public IncidentService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<IncidentType>> GetTypeIncidentsAsync()
        {
            return await _context.IncidentTypes.ToListAsync();
        }

        public async Task<List<Priority>> GetPrioritesAsync()
        {
            return await _context.Priorities.ToListAsync();
        }

        // Méthode pour ajouter un incident
        public async Task<Incident> AddIncidentAsync(Incident incident)
        {
            if (incident == null)
                throw new ArgumentNullException(nameof(incident), "L'incident ne peut pas être nul");

            // Logique métier : vérifier les champs nécessaires
            if (string.IsNullOrWhiteSpace(incident.Title) || string.IsNullOrWhiteSpace(incident.Description))
            {
                throw new ArgumentException("Le titre et la description de l'incident sont requis.");
            }

            // Ajout de l'incident à la base de données
            _context.Incidents.Add(incident);
            await _context.SaveChangesAsync();

            return incident;  // Retourner l'incident ajouté
        }
    }

}
