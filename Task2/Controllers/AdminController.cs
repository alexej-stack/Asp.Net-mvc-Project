using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using System.Web.Mvc;
using Task2.Models;

namespace Filters.Controllers
{


    public class AdminController : Controller
    {
        private UserManager<ApplicationUser> userManager;

        public AdminController(UserManager<ApplicationUser> usrMgr)
        {
            userManager = usrMgr;
        }

        public ActionResult Index()
        {
            return View();
        }

    }
    }
