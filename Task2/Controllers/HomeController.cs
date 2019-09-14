using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Task2.Models;

namespace Task2.Controllers
{
    public class HomeController : Controller
    {

        AspContext db = new AspContext();
        [Authorize(Roles = "user,admin")]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View(db.AspNetUsers);
        }

       
        [Authorize(Roles = "user,admin")]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
   
        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.ListEmployee = this.db.AspNetUsers.ToList();
            return View();
        }
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
        [HttpPost]

        public ActionResult Index(FormCollection formCollection)
        {
            string[] ids = formCollection["Id"].Split(new char[] { ',' });
            foreach (string id in ids)
            {
                var employee = this.db.AspNetUsers.Find(id);
                this.db.AspNetUsers.Remove(employee);
                this.db.SaveChanges();
                AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                return RedirectToAction("Index", "Home");
                
                //return RedirectToAction("Index", "Home");
            }
            
            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult MyAction()
        {
            ViewBag.ListEmployee = this.db.AspNetUsers.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult MyAction(FormCollection formCollection, string action)
        {
            if (action == "Del")
            {
                string[] ids = formCollection["Id"].Split(new char[] { ',' });
                foreach (string id in ids)
                {
                    var employee = this.db.AspNetUsers.Find(id);
                    this.db.AspNetUsers.Remove(employee);
                    this.db.SaveChanges();
                    
                    AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                    return RedirectToAction("Index", "Home");

                    //return RedirectToAction("Index", "Home");
                }

                return RedirectToAction("Index");
            }
            else if (action == "Lock")
            {
                string[] ids = formCollection["Id"].Split(new char[] { ',' });
                foreach (string id in ids)
                {
                    var employee = this.db.AspNetUsers.Find(id);
                    /*if (!(User.Identity.IsAuthenticated))
                    {*/
                   
                    AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);

                    
                    employee.IsEnable = "False";
                    //await UserManager.AddToRoleAsync(employee.Id, "user");
                    //UserManager.AddToRole(employee.Id, "Baned");
                    // employee.LockoutEnabled = 1;
                    this.db.SaveChanges();
                    return View("Lockout");
                }

                return RedirectToAction("Index");
            }
            else if (action == "UnLock")
            {
                string[] ids = formCollection["Id"].Split(new char[] { ',' });
                foreach (string id in ids)
                {
                    var employee = this.db.AspNetUsers.Find(id);
                    employee.IsEnable = "True";
                    // employee.LockoutEnabled = 1;
                    this.db.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        public ActionResult Chat()
        {
            return View();
        }
    }
}