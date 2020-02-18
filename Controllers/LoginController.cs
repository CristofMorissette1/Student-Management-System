using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplicationWithMSQL.Models;

namespace WebApplicationWithMSQL.Controllers
{
    public class LoginController : Controller
    {

        Entities db = new Entities();

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(user objchk)
        {

            if(ModelState.IsValid) 
            {

                var obj = db.users.Where(a => a.username.Equals(objchk.username) && a.password.Equals(objchk.password)).FirstOrDefault();

                if (obj != null)
                {
                    Session["UserID"] = obj.id.ToString();
                    Session["UserName"] = obj.username.ToString();
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "The UserName or Password is Incorrect");

                }
            }

            return View(objchk);
        }

        public ActionResult LogOut()
        {

            Session.Clear();
            return RedirectToAction("Index", "Login");
        }
    }
}