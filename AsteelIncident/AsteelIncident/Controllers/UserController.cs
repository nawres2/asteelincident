using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AsteelIncident.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult DashboardUser()
        {
            return View();
        }
    }
}