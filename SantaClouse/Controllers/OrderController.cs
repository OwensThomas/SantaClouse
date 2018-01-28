using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SantaClouse.Models;

namespace SantaClouse.Controllers
{
    public class RequestKidController : Controller
    {
        // GET: RequestKid
        public ActionResult Index()
        {
            if (Session["IsAdmin"] != null)
            {
                MongoDB db = new MongoDB();
                var requests_kids = db.GetAllOreders();
                Orders model = new Orders();
                model.EntityList = requests_kids.ToList();
                return View(model);
            }
            else
            {
                return RedirectToAction("../Users/Login");
            }
        }

        public ActionResult Details(string id)
        {
            MongoDB db = new MongoDB();
            var orders = db.GetOrder(id);
            Orders model = new Orders();
            ViewBag.Id = orders.ID;
            ViewBag.KidName = orders.Kid;
            ViewBag.Date = orders.requestDate.ToString("dd-MMM-yyyy");


            switch (orders.TypeStatus)
            {
                case Status.inProgress:
                    ViewBag.Status = "inProgress";
                    break;
                case Status.ReadyToSend:
                    ViewBag.Status = "ReadyToSend";
                    break;
                case Status.Done:
                    ViewBag.Status = "Done";
                    break;

                default:
                    break;
            }

            model.ToyList = orders.ToyKids;

            return View(model);
        }

    }

}