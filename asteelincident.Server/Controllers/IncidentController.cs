using asteelincident.Server.Data;
using asteelincident.Server.Models;
using asteelincident.Server.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Text;
using System.Text.Json;

namespace asteelincident.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncidentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        private readonly IncidentService _incidentService;

        // Injection du service dans le contrôleur
        public IncidentsController(ApplicationDbContext context, IncidentService incidentService)
        {
            _context = context;
            _incidentService = incidentService;
        }
        [HttpPost("AddIncident")]
        public async Task<IActionResult> AddIncident([FromBody] Incident incidentData)
        {
            if (incidentData == null)
            {
                return BadRequest("Les données de l'incident sont invalides.");
            }
            try
            {
                // Déboguer l'objet reçu
                Console.WriteLine($"Incident reçu: {System.Text.Json.JsonSerializer.Serialize(incidentData)}");
                var incident = new Incident
                {
                    Title = incidentData.Title,
                    Description = incidentData.Description,
                    IncidentTypeID = incidentData.IncidentTypeID,
                    PriorityID = incidentData.PriorityID,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    Status = "Open",
                    CreatedBy = incidentData.CreatedBy != 0 ? incidentData.CreatedBy : 1003
                };
                _context.Incidents.Add(incident);
                await _context.SaveChangesAsync();
                return Ok(new { message = "Incident ajouté avec succès!", incidentId = incident.IncidentID });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur: {ex.Message}");
                return StatusCode(500, $"Erreur interne du serveur: {ex.Message}");
            }
        }

        [HttpGet("GetTypeIncident")]
        public async Task<IActionResult> GetTypeIncident()
        {
            var types = await _context.IncidentTypes.Select(t => new { t.TypeID, t.TypeName }).ToListAsync();
            return Ok(types);
        }

        // Récupérer la liste des priorités
        [HttpGet("GetPriorite")]
        public async Task<IActionResult> GetPriorite()
        {
            var priorites = await _context.Priorities.Select(p => new { p.PriorityID, p.PriorityName }).ToListAsync();
            return Ok(priorites);
        }


        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<object>>> GetAllIncidents()
        {
            var incidents = await _context.Incidents
                .Include(i => i.IncidentType)
                .Include(i => i.Priority)  
                .Include(i=>i.CreatedByUser)
                .Select(i => new
                {
                    i.IncidentID,
                    i.Title,
                    IncidentType = i.IncidentType != null ? i.IncidentType.TypeName : "Non défini", 
                    Status = i.Status ?? "Non défini",
                    Priority = i.Priority != null ? i.Priority.PriorityName : "Non défini",
                    CreatedBy = i.CreatedByUser != null ? i.CreatedByUser.FullName : "Utilisateur inconnu", // ✅ Récupérer le nom

                    CreatedAt = i.CreatedAt != null ? i.CreatedAt : new DateTime(2000, 1, 1), // Valeur,par défaut
                    UpdatedAt = i.UpdatedAt
                })
                .ToListAsync();

            return Ok(incidents);
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Incident>>> SearchIncidents(
            [FromQuery] string? type,
            [FromQuery] string? priority,
            [FromQuery] string? status,
            [FromQuery] string? searchText)
        {
            var query = _context.Incidents.AsQueryable();

            if (!string.IsNullOrEmpty(type))
                query = query.Where(i => i.IncidentType.TypeName == type);

            if (!string.IsNullOrEmpty(priority))
                query = query.Where(i => i.Priority.PriorityName == priority);

            if (!string.IsNullOrEmpty(status))
                query = query.Where(i => i.Status == status);

            if (!string.IsNullOrEmpty(searchText))
                query = query.Where(i => i.Title.Contains(searchText));

            var result = await query.ToListAsync();
            return Ok(result);
        }

        [HttpGet("stats")]
        public IActionResult GetStatistics()
        {
            var totalIncidents = _context.Incidents.Count();

            var incidentsParType = _context.Incidents
       .GroupBy(i => i.IncidentType)
       .Select(g => new { type = g.Key, nombre = g.Count() })
       .ToList();



            return Ok(new { totalIncidents, incidentsParType });
        }

        [HttpGet("evolution")]
        public IActionResult GetEvolution()
        {
            // Obtenir les données des 30 derniers jours (ou une autre période)
            var startDate = DateTime.Now.AddDays(-30);

            // Regrouper les incidents par date et statut
            var evolution = _context.Incidents
                .Where(i => i.CreatedAt >= startDate)
                .GroupBy(i => new {
                    Date = i.CreatedAt.Date,
                    Status = i.Status
                })
                .Select(g => new {
                    g.Key.Date,
                    g.Key.Status,
                    Count = g.Count()
                })
                .ToList();

            // Transformer les données dans le format attendu par le frontend
            var dates = evolution.Select(e => e.Date).Distinct().OrderBy(d => d).ToList();

            var result = dates.Select(date => new {
                date,
                nouveaux = evolution.FirstOrDefault(e => e.Date == date && e.Status == "Open")?.Count ?? 0,
                enCours = evolution.FirstOrDefault(e => e.Date == date && e.Status == "In Progress")?.Count ?? 0,
                resolus = evolution.FirstOrDefault(e => e.Date == date && e.Status == "Closed")?.Count ?? 0
            }).ToList();

            return Ok(result);
        }


        [HttpGet("export-csv")]
        public async Task<IActionResult> ExportCsv()
        {
            try
            {
                // Récupérer les statistiques
                var stats = await GetStatsData();  // Méthode à implémenter selon votre logique existante

                // Récupérer les données d'évolution
                var evolution = await GetEvolutionData();  // Méthode à implémenter selon votre logique existante

                // Créer le contenu CSV
                var csvContent = new StringBuilder();
                csvContent.AppendLine("Rapport d'incidents");
                csvContent.AppendLine();

                // Ajouter la section Total
                csvContent.AppendLine($"Total d'incidents;{stats.TotalIncidents}");
                csvContent.AppendLine();

                // Ajouter la section Incidents par type
                csvContent.AppendLine("Incidents par type");
                csvContent.AppendLine("Type;Nombre;Pourcentage");

                int totalByType = stats.IncidentsParType.Sum(i => i.Nombre);
                foreach (var incident in stats.IncidentsParType)
                {
                    double percentage = Math.Round((double)incident.Nombre / totalByType * 100, 1);
                    csvContent.AppendLine($"{incident.Type.TypeName};{incident.Nombre};{percentage.ToString("0.0", CultureInfo.InvariantCulture)}%");
                }

                csvContent.AppendLine();

                // Ajouter la section Évolution du statut des incidents
                csvContent.AppendLine("Évolution du statut des incidents");
                csvContent.AppendLine("Date;Nouveaux;En cours;Résolus");

                foreach (var item in evolution.OrderBy(e => e.Date))
                {
                    string formattedDate = item.Date.ToString("dd/MM/yyyy");
                    csvContent.AppendLine($"{formattedDate};{item.Nouveaux};{item.EnCours};{item.Resolus}");
                }

                // Générer le fichier CSV
                byte[] bytes = Encoding.UTF8.GetBytes(csvContent.ToString());
                var memoryStream = new MemoryStream(bytes);

                // Retourner le fichier
                return File(memoryStream, "text/csv", $"rapport_incidents_{DateTime.Now:yyyy-MM-dd}.csv");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erreur lors de l'exportation: {ex.Message}");
            }
        }

        // Méthodes privées pour récupérer les données
        // Ces méthodes doivent être adaptées à votre modèle de données et à votre logique existante

        private async Task<StatsViewModel> GetStatsData()
        {
            // Implémentez cette méthode pour récupérer les mêmes données que votre endpoint stats
            // C'est probablement similaire à votre implémentation actuelle de l'endpoint stats

            // Exemple fictif :
            return new StatsViewModel
            {
                TotalIncidents = _context.Incidents.Count(),
                IncidentsParType = await _context.Incidents
                    .GroupBy(i => i.IncidentTypeID)
                    .Select(g => new IncidentTypeCount
                    {
                        Type = _context.IncidentTypes.Find(g.Key),
                        Nombre = g.Count()
                    })
                    .ToListAsync()
            };
        }

        private async Task<List<EvolutionViewModel>> GetEvolutionData()
        {
            // Implémentez cette méthode pour récupérer les mêmes données que votre endpoint evolution
            // C'est probablement similaire à votre implémentation actuelle de l'endpoint evolution

            // Exemple fictif :
            var result = new List<EvolutionViewModel>();

            // Code pour remplir result avec les données d'évolution...

            return result;
        }
    }

    // Modèles de vue (à adapter selon vos modèles existants)
    public class StatsViewModel
    {
        public int TotalIncidents { get; set; }
        public List<IncidentTypeCount> IncidentsParType { get; set; }
    }

    public class IncidentTypeCount
    {
        public IncidentType Type { get; set; }
        public int Nombre { get; set; }
    }

    public class EvolutionViewModel
    {
        public DateTime Date { get; set; }
        public int Nouveaux { get; set; }
        public int EnCours { get; set; }
        public int Resolus { get; set; }
    
    }
}
