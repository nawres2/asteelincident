using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AsteelIncident.Models
{
    public class IncidentDashboardViewModel
    {
        public int TotalIncidents { get; set; }
        public int OpenIncidents { get; set; }
        public int InProgressIncidents { get; set; }
        public int ResolvedIncidents { get; set; }
        public List<Incident> RecentIncidents { get; set; }
    }

}