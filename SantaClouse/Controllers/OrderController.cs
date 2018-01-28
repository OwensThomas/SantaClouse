using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SantaClouse.Models;

namespace SantaClouse.Controllers
{
    public class OrderController : Controller
    {
        // GET: RequestKid
        public ActionResult Index()
        {
            if (Session["IsAdmin"] != null)
            {
                MongoDB db = new MongoDB();
                var orders = db.GetAllOreders();
                Orders model = new Orders();
                model.EntityList = orders.ToList();
                return View(model);
            }
            else
            {
                return RedirectToAction("../Users/Login");
            }
        }

        public ActionResult Details(string id)
        {
            if (Session["IsAdmin"] != null)
            {
                MongoDB db = new MongoDB();
                var orders = db.GetOrder(id);
                Orders model = new Orders();
                ViewBag.Id = orders.ID;
                ViewBag.Kid = orders.Kid;
                ViewBag.requestDate = orders.requestDate.ToString("dd-MMM-yyyy");
                switch (orders.Status)
                {
                    case TypeStatus.inProgress:
                        ViewBag.Status = "inProgress";
                        break;
                    case TypeStatus.ReadyToSend:
                        ViewBag.Status = "ReadyToSend";
                        break;
                    case TypeStatus.Done:
                        ViewBag.Status = "Done";
                        break;
                    default:
                        break;
                }

                model.ToyList = orders.ToyKids;

                return View(model);
            }
            else
            {
                return RedirectToAction("../Users/Login");
            }

        }
        public ActionResult Edit(string id)
        {
            if (Session["IsAdmin"] != null && Session["IsAdmin"].Equals(false))
            {
                MongoDB db = new MongoDB();
                var orders = db.GetOrder(id);
                //utile per estrarre tutti i giochi richiesti dal bambino
                Orders modelToy = new Orders();
                modelToy.ToyList = orders.ToyKids;
                Toy toy = new Toy();
                //utile per passare alla view lo stato dell ordine
                Order model = new Order();
                model.Status = orders.Status;
                return View(model);
            }
            else
            {
                return RedirectToAction("../Users/Login");
            }
        }

        public ActionResult Save(TypeStatus typeStatus, string id)
        {
            if (Session["IsAdmin"] != null || typeStatus.Equals(TypeStatus.Done))
            {
                    if (string.IsNullOrWhiteSpace(typeStatus.ToString()))
                {
                    throw new MissingFieldException("fill all the fields");
                }
                bool result;
                MongoDB db = new MongoDB();
                var Order = db.GetOrder(id);
                Orders toyModel = new Orders();
                toyModel.ToyList = Order.ToyKids;
                Toy toy = new Toy();
                var query = toyModel.ToyList.GroupBy(x => x)
                                    .Select(y => new { Element = y.Key, Counter = y.Count() })
                                    .ToList();
                foreach (var toyRequest in query)
                {
                    toy = db.GetToy(toyRequest.Element.ToyName);
                    if (toy.Amount <= toyRequest.Counter)
                    {
                        ModelState.AddModelError("", "Order no Avaible");
                        return RedirectToAction("Details", id);
                    }
                }

                if (string.IsNullOrWhiteSpace(id))
                {
                    Order requestkid = new Order();

                }

                result = db.UpdateOrder(new Order
                {
                    ID = id,
                    Status = typeStatus
                });


                foreach (var toyRequest in toyModel.ToyList)
                {

                    toy = db.GetToy(toyRequest.ToyName);
                    result = db.UpdateToy(toy);
                    if (toy.Amount == 0)
                    {
                        db.RemoveToy(toy.ID);
                    }
                }

                return RedirectToAction("Index", new { result = result });
            }
         return RedirectToAction("../Users/Login");
        }
               
    }
}