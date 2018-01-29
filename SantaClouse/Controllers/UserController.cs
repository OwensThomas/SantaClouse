using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SantaClouse;
using System.Security.Cryptography;
using MongoDB = SantaClouse.MongoDB;
using System.Text;

namespace SantaClouse.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Login()
        {
            return View();
        }
        //SHA512 sha512 = new SHA512();

        //[HttpPost]
        //public ActionResult Login(User user)
        //{
        //    byte[] a = SHA512.Create().ComputeHash(Encoding.UTF8.GetBytes(user.Password));
        //    string hash = BitConverter.ToString(a).Replace("-", "").ToLower(); //byte[] -> hex -> string MAIUSCOLA 
        //    MongoDB db = new MongoDB();
        //    var account = db.GetUser(user);
        //    if (account != null)
        //    {
        //        Session["Email"] = account.Email.ToString();
        //        Session["ID"] = account.ID.ToString();
        //        Session["ScreenName"] = account.ScreenName.ToString();
        //        Session["IsAdmin"] = account.IsAdmin.ToString();
        //        return RedirectToAction($"../Home");
        //    }
        //    else
        //    {

        string Encrypt(string text)
        {
            byte[] data = Encoding.UTF8.GetBytes(text);
            byte[] resultHash;
            SHA512 shaM = new SHA512Managed();
            resultHash = shaM.ComputeHash(data);
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < resultHash.Length; i++)
            {
                result.Append(resultHash[i].ToString("X2"));
            }
            return result.ToString().ToLower();
        }
        [HttpPost]
        public ActionResult Login(User user)
        {
            user.Password = Encrypt(user.Password);
            MongoDB db = new MongoDB();
            var account = db.GetUser(user);
            if (account != null)
            {
                Session["ScreenName"] = account.ScreenName.ToString();
                Session["IsAdmin"] = Convert.ToBoolean(account.IsAdmin.ToString());
                return RedirectToAction("../Home");
            }
            else
            {
                ModelState.AddModelError("", "Username or Password Error");
            }
            return View();
        }

        public ActionResult Logout()
        {
            if (Session["ID"] != null)
            {
                Session.Clear();
                return RedirectToAction("Logout");
            }
            return View();
        }

    }
}
