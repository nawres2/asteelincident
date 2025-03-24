using asteelincident.Server.Data;
using asteelincident.Server.Models;
using asteelincident.Server.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace asteelincident.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IndexController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IndexService _incidentService;

        // Un seul constructeur pour les deux services
        public IndexController(ApplicationDbContext context, IndexService incidentService)
        {
            _context = context;
            _incidentService = incidentService;
        }

        [HttpGet("index")]
        public async Task<IActionResult> GetIndexData()
        {
            // Récupérer les incidents depuis la base de données
            var incidents = await _context.Incidents.ToListAsync();

            // Statistiques basées sur les incidents
            var stats = new
            {
                Ouvert = incidents.Count(i => i.Status == "Ouvert"),
                Enattente = incidents.Count(i => i.Status == "En attente"),

                Resolu = incidents.Count(i => i.Status == "Résolu"),
                // Ajoutez d'autres statistiques si nécessaire
            };


            // Retourner les données sous forme d'objet JSON
            return Ok(new
            {

                stats,
                incidents
            });
        }
    }

}

