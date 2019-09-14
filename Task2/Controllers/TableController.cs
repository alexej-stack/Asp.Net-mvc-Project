using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Task2.Models;
namespace Task2.Controllers
{
    public class TableController : Controller
    {
        // GET: Table
        public ActionResult Index()
        {
            var entities = new AccountController();

            return View(entities.ViewData.ToList());
           // return View();
        }
    }
}