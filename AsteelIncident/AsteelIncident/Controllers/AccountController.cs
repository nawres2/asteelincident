using AsteelIncident.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace AsteelIncident.Controllers
{

    public class AccountController : Controller
    {
        private Modelincident db = new Modelincident();

        public ActionResult Login()
        {
            return View();
        }

      

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            var user = db.Users.FirstOrDefault(u => u.Username == username && u.Password == password);
            if (user != null)
            {
                FormsAuthentication.SetAuthCookie(user.Username, false);

                
                int userRole = (int)user.RoleID;

                
                Session["UserID"] = user.UserID;
                Session["Username"] = user.Username;
                Session["UserRoleID"] = userRole;

               
                if (userRole == 1) 
                    return RedirectToAction("Dashboard", "Admin");
                else if (userRole ==2) 
                    return RedirectToAction("Tasks", "User");
                else
                    return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Error = "Nom d'utilisateur ou mot de passe incorrect.";
                return View();
            }
        }



    }
}