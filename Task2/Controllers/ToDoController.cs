using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Task2.Models;

namespace Task2.Controllers
{
    public class ToDo : Controller
    {
        private AspContext db = new AspContext();
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.ListEmployee = this.db.AspNetUsers.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection formCollection)
        {
            string[] ids = formCollection["Id"].Split(new char[] { ',' });
            foreach (string id in ids)
            {
                var employee = this.db.AspNetUsers.Find(int.Parse(id));
                this.db.AspNetUsers.Remove(employee);
                this.db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}