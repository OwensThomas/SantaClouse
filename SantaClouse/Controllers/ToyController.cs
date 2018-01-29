using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SantaClouse;
using SantaClouse.Models;


namespace SantaClouse.Controllers
{
    public class ToysController : Controller
    {
        // GET: Toys
        public ActionResult Index()
        {
            if (Session["IsAdmin"] != null)
            {
                MongoDB db = new MongoDB();
                var toys = db.GetAllToys();
                Toys model = new Toys();
                model.EntityList = toys.ToList();
                return View(model);
            }
            else
            {
                return RedirectToAction("Login", "Users");
            }
        }
    }
}

