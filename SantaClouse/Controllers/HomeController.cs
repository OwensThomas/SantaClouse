using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;

namespace SantaClouse.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Descrizione della pagina.";

            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Contattaci pure qui, però non assicuro che sarai ricontattato.";
            return View();
        }
    }
}