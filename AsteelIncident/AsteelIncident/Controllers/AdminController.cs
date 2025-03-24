using AsteelIncident.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AsteelIncident.Controllers
{
    public class AdminController : Controller
    {
        private Modelincident db = new Modelincident();


        // GET: Admin
        public ActionResult DashboardAdmin()
        {
            var model = new IncidentDashboardViewModel
            {
                TotalIncidents = db.Incidents.Count(),
                OpenIncidents = db.Incidents.Count(i => i.Status == "Ouvert"),
                InProgressIncidents = db.Incidents.Count(i => i.Status == "En cours"),
                ResolvedIncidents = db.Incidents.Count(i => i.Status == "Résolu"),
                RecentIncidents = db.Incidents
                    .OrderByDescending(i => i.CreatedAt)
                    .Take(5)
                    .ToList()
            };

            return View(model);
        }
       
    }
}
