using System.Linq;
using System.Web.Mvc;
using Task2.Models;
namespace Task2.Controllers
{
   public class DataGridController : Controller
   {
        private AspContext db = new AspContext();
        public ActionResult UserView()
        {

            ViewBag.dataSource = db.UserProfiles.ToList();

            return View();
        }

    }
}