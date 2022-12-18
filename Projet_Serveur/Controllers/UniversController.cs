using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Projet_Serveur.Controllers
{
    public class UniversController : Controller
    {
        // On retourne la vue notre univers
        public ActionResult Index()
        {
            return View("Index");
        }
    }
}