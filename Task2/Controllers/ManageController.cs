using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using PagedList;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Newtonsoft.Json.Linq;
using Task2.Models;
using Korzh.EasyQuery.Linq;

namespace Task2.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public ManageController()
        {
        }

        public ManageController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(FormCollection form)
        {
            var comment = form["Comment"].ToString();
            var companyId = Int32.Parse(form["CompanyId"]);
            var rating = int.Parse(form["Rating"]);

            CompanyComment compComment = new CompanyComment()
            {
                CompanyId = companyId,
                Comments = comment,
                Rating = rating,
                ThisDateTime = DateTime.Now
            };

            db.CompanyComments.Add(compComment);
            db.SaveChanges();

            return RedirectToAction("Details", "Manage", new { id = companyId });
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set 
            { 
                _signInManager = value; 
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        AspContext db = new AspContext();
        // GET: /Manage/Bonus
        [HttpGet]
        public ActionResult Bonus()
        {
            ViewBag.ListBonus = this.db.Bonus.ToList();
            return View();
           
        }
        //
        


     
        [HttpGet]
        public ActionResult InPlace()
        {
            
            ViewBag.ListUser = this.db.UserProfiles.ToList();

             //return View();
              List<UserProfile> list = new List<UserProfile>();
              using (AspContext dc = new AspContext())
              {
                  var v = (from a in dc.UserProfiles.AsEnumerable()
                           orderby a.ID


                           select new UserProfile 
                           {
                               ID = a.ID,
                               FirstName = a.FirstName,
                                LastName = a.LastName,
                                 DOB =a.DOB,

                           });
                  list = v.ToList();
              }
             return View(list);
         }

        [HttpPost]
        public ActionResult saveuser(int id, string propertyName, string value)
        {
            var status = false;
            var message = "";

            //Update data to database 
            using (AspContext dc = new AspContext())
            {
                var user = dc.UserProfiles.Find(id);
                if (user != null)
                {
                    dc.Entry(user).Property(propertyName).CurrentValue = value;
                    dc.SaveChanges();
                    status = true;
                }
                else
                {
                    message = "Error!";
                }
            }

            var response = new { value = value, status = status, message = message };
            JObject o = JObject.FromObject(response);
            return Content(o.ToString());
        }

        [HttpPost]
        public ActionResult CreateProfile()
        {
            return View(new UserProfile());
        }
        // GET: /Manage/Company
        [HttpGet]
        public ActionResult Company()
        {
            IQueryable<Company>  ListCompanies =db.Companies;
            if (User.Identity.GetUserId() != null )
            {
                ListCompanies = ListCompanies.Where(p => p.aspNetUser == User.Identity.GetUserId());
            }
            return View();
        }
        [HttpPost]
        public ActionResult CreateProfile(UserProfile userProfile)
        {
            return View(userProfile);
        }
        public ActionResult CompanyCreate()
        {
           
            return View(new Company());
            
        }

        [HttpPost]
        public ActionResult CompanyCreate(Company company)
        {
            if (ModelState.IsValid)
            {
                Company user = null;
                using (AspContext db = new AspContext())
                {
                    user = db.Companies/*.Include(p => p.AspNetUser)*/.FirstOrDefault(u => u.ID == company.ID );
                }
               /* if (user == null)
                {
              */
                    using (AspContext db = new AspContext())
                    {
                        var id= User.Identity.GetUserId();
                        db.Companies.Add(new Company { tName = company.tName, Description = company.Description,Tematic= company.Tematic,aspNetUser= id/*aspNetUser=new AspNetUser()*/ });
                        db.SaveChanges();

                        user = db.Companies.Where(u => u.tName == company.tName && u.Description == company.Description && u.Tematic == company.Tematic && u.aspNetUser == id).FirstOrDefault();
                    }
                  
                    if (user != null)
                    {

                        return RedirectToAction("Index", "Manage");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Пользователь с таким логином уже существует");
                }
              
            
            return View(company);
        }
       
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company course = db.Companies.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }
        public ActionResult CreateBonus()
        {

            return View();
        }
        public ActionResult Create()
        {
           
            return View();
        }
        public ActionResult ChangeTheme()
        {
            if (Request.Cookies["theme"].Value == null)
            {
                Response.Cookies["theme"].Value = "dark";
            }
            else
            {
                if (Request.Cookies["theme"].Value == "dark")
                {
                    Response.Cookies["theme"].Value = "light";
                }
                else if (Request.Cookies["theme"].Value == "light")
                {
                    Response.Cookies["theme"].Value = "dark";
                }
            }

            return RedirectToAction("Index","Manage");
        }
        // GET: Course/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company course = db.Companies.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: Course/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Company course = db.Companies.Find(id);
            db.Companies.Remove(course);
            db.SaveChanges();
            return RedirectToAction("Index","Manage");
        }
        [ ValidateInput(false)]
        public ActionResult Details(int? id)
        {
            AspContext db = new AspContext();
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = db.Companies.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            ViewBag.CompanyId = id.Value;

            var comments = db.CompanyComments.Where(d => d.CompanyId.Equals(id.Value)).ToList();
            ViewBag.Comments = comments;

            var ratings = db.CompanyComments.Where(d => d.CompanyId.Equals(id.Value)).ToList();
            if (ratings.Count() > 0)
            {
                var ratingSum = ratings.Sum(d => d.Rating.Value);
                ViewBag.RatingSum = ratingSum;
                var ratingCount = ratings.Count();
                ViewBag.RatingCount = ratingCount;
            }
            else
            {
                ViewBag.RatingSum = 0;
                ViewBag.RatingCount = 0;
            }

            return View(company);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateBonus([Bind(Include = "Id,Name,Price,aspNetUser_id,Company")]Bonus bonus)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Bonus.Add(bonus);
                    
                    db.SaveChanges();
                    return RedirectToAction("Index", "Manage");
                }
            }
            catch (RetryLimitExceededException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.)
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
            return View(bonus);
        }
        public ActionResult List()
        {
            AspContext db = new AspContext();
            return View(db.Companies.ToList());
        }
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "tName,Description,UserImage,Video,Tematic,aspNetUser")]Company company)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    company.Votes = "0,0,0,0,0";
                    db.SaveChanges();
                    var id = User.Identity.GetUserId();
                    
                    db.Companies.Add(company);
                    db.SaveChanges();
                    company.aspNetUser = id;
                    db.SaveChanges();
                    return RedirectToAction("Index","Manage");
                }
            }
            catch (RetryLimitExceededException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.)
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
           return View(company);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var courseToUpdate = db.Companies.Find(id);
            if (TryUpdateModel(courseToUpdate, "",
               new string[] { "Name", "Tematic", "Description" }))
            {
                try
                {
                    db.SaveChanges();

                    return RedirectToAction("Index","Manage");
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            return View(courseToUpdate);
        }


        public ActionResult ButtonAddBonus1(Company Company)
        {   var  ID= User.Identity.GetUserId();
            var  company= db.Companies.Find(Company.ID);
            var  bonuses= db.Bonus.FirstOrDefault(i=>i.Company==company);
            var  user = db.AspNetUsers.Find(ID);
            if (company != null&&user!=null)
            {
                user.Bonus.Add(bonuses);
                db.SaveChanges();
            }
            return RedirectToAction("Index", "Manage"); 
        }
        public ActionResult WorkAction(FormCollection formCollection, string action)
        {
            if (action == "Del")
            {
                string[] ids = formCollection["ID"].Split(new char[] { ',' });
                foreach (string id in ids)
                {
                    var company = this.db.Companies.Find(Int32.Parse(id));
                    this.db.Companies.Remove(company);
                    this.db.SaveChanges();

                   
                    return RedirectToAction("Index", "Manage");

                    //return RedirectToAction("Index", "Home");
                }

             
            }
            
            return RedirectToAction("Index", "Manage");
        }
       
        //
        // POST: /Manage/RemoveLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemoveLogin(string loginProvider, string providerKey)
        {
            ManageMessageId? message;
            var result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(), new UserLoginInfo(loginProvider, providerKey));
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                message = ManageMessageId.RemoveLoginSuccess;
            }
            else
            {
                message = ManageMessageId.Error;
            }
            return RedirectToAction("ManageLogins", new { Message = message });
        }

        //
        // GET: /Manage/AddPhoneNumber
        public ActionResult AddPhoneNumber()
        {
            return View();
        }


        //
        // GET: /Manage/ChangePassword
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Manage/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
            }
            AddErrors(result);
            return View(model);
        }

        //
        // GET: /Manage/SetPassword
        public ActionResult SetPassword()
        {
            return View();
        }

        //
        // POST: /Manage/SetPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SetPassword(SetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
                if (result.Succeeded)
                {
                    var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                    if (user != null)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    }
                    return RedirectToAction("Index", new { Message = ManageMessageId.SetPasswordSuccess });
                }
                AddErrors(result);
            }

            // Это сообщение означает наличие ошибки; повторное отображение формы
            return View(model);
        }

        //
        // GET: /Manage/ManageLogins
        public async Task<ActionResult> ManageLogins(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.RemoveLoginSuccess ? "Внешнее имя входа удалено."
                : message == ManageMessageId.Error ? "Произошла ошибка."
                : "";
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user == null)
            {
                return View("Error");
            }
            var userLogins = await UserManager.GetLoginsAsync(User.Identity.GetUserId());
            var otherLogins = AuthenticationManager.GetExternalAuthenticationTypes().Where(auth => userLogins.All(ul => auth.AuthenticationType != ul.LoginProvider)).ToList();
            ViewBag.ShowRemoveButton = user.PasswordHash != null || userLogins.Count > 1;
            return View(new ManageLoginsViewModel
            {
                CurrentLogins = userLogins,
                OtherLogins = otherLogins
            });
        }
        
        public ViewResult Index(string sortOrder, string currentFilter,int? page)
        {
            ViewBag.ListCompanies = this.db.Companies.ToList();
            ViewBag.List = this.db.Companies.ToList();
            ViewBag.ListBonus = this.db.Bonus.ToList();
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
          //  ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";


            var companies = from s in db.Companies 
                           select s ;
            
            switch (sortOrder)
            {
                case "name_desc":
                    companies = companies.OrderByDescending(s => s.tName);
                    break;
              /*  case "Date":
                    companies = companies.OrderBy(s => s.EnrollmentDate);
                    break;
                case "date_desc":
                    companies = companies.OrderByDescending(s => s.EnrollmentDate);
                    break;*/
                default:  // Name ascending 
                    companies = companies.OrderBy(s => s.tName);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            var ID = User.Identity.GetUserId();
            return View(companies.Where(u => u.aspNetUser == ID).ToPagedList(pageNumber, pageSize));
        }

        //
        // POST: /Manage/LinkLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LinkLogin(string provider)
        {
            // Запрос перенаправления к внешнему поставщику входа для связывания имени входа текущего пользователя
            return new AccountController.ChallengeResult(provider, Url.Action("LinkLoginCallback", "Manage"), User.Identity.GetUserId());
        }

        //
        // GET: /Manage/LinkLoginCallback
        public async Task<ActionResult> LinkLoginCallback()
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync(XsrfKey, User.Identity.GetUserId());
            if (loginInfo == null)
            {
                return RedirectToAction("ManageLogins", new { Message = ManageMessageId.Error });
            }
            var result = await UserManager.AddLoginAsync(User.Identity.GetUserId(), loginInfo.Login);
            return result.Succeeded ? RedirectToAction("ManageLogins") : RedirectToAction("ManageLogins", new { Message = ManageMessageId.Error });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                _userManager.Dispose();
                _userManager = null;
            }

            base.Dispose(disposing);
        }

#region Вспомогательные приложения
        // Используется для защиты от XSRF-атак при добавлении внешних имен входа
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        private bool HasPhoneNumber()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PhoneNumber != null;
            }
            return false;
        }

        public enum ManageMessageId
        {
            AddPhoneSuccess,
            ChangePasswordSuccess,
            SetTwoFactorSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            RemovePhoneSuccess,
            Error
        }
      /*  public JsonResult SendRating(string r, string s, string id, string url)
        {
            int autoId = 0;
            Int16 thisVote = 0;
            Int16 sectionId = 0;
            Int16.TryParse(s, out sectionId);
            Int16.TryParse(r, out thisVote);
            int.TryParse(id, out autoId);

            if (!User.Identity.IsAuthenticated)
            {
                return Json("Not authenticated!");
            }

            if (autoId.Equals(0))
            {
                return Json("Sorry, record to vote doesn't exists");
            }

            switch (s)
            {
                case "5": 
                    // check if he has already voted
                    var isIt = db.VoteLogs.Where(v => v.SectionId == sectionId &&
                       /* v.UserName.Equals(User.Identity.Name, StringComparison.CurrentCultureIgnoreCase) && v.VoteForId == autoId).FirstOrDefault();
                    if (isIt != null)
                    {
                       
                        HttpCookie cookie = new HttpCookie(url, "true");
                        Response.Cookies.Add(cookie);
                        return Json("<br />You have already rated this post, thanks !");
                    }

                    var sch = db.Companies.Where(sc => sc.ID == autoId).FirstOrDefault();
                    if (sch != null)
                    {
                        object obj = sch.Votes;

                        string updatedVotes = string.Empty;
                        string[] votes = null;
                        if (obj != null && obj.ToString().Length > 0)
                        {
                            string currentVotes = obj.ToString(); 
                            votes = currentVotes.Split(',');
                           if (votes.Length.Equals(5))
                            {
                                 int currentNumberOfVote = int.Parse(votes[thisVote - 1]);
                            
                                currentNumberOfVote++;
                           
                                votes[thisVote - 1] = currentNumberOfVote.ToString();
                            }
                            else
                            {
                                votes = new string[] { "0", "0", "0", "0", "0" };
                                votes[thisVote - 1] = "1";
                            }
                        }
                        else
                        {
                            votes = new string[] { "0", "0", "0", "0", "0" };
                            votes[thisVote - 1] = "1";
                        }

                        // concatenate all arrays now
                        foreach (string ss in votes)
                        {
                            updatedVotes += ss + ",";
                        }
                        updatedVotes = updatedVotes.Substring(0, updatedVotes.Length - 1);

                        db.Entry(sch).State = EntityState.Modified;
                        sch.Votes = updatedVotes;
                        db.SaveChanges();
                        Company co = db.Companies.Find(id);
                        VoteLog vm = new VoteLog()
                        {
                            Active = true,
                            SectionId = Int16.Parse(s),
                            UserName = co.tName,
                            Vote = thisVote,
                            VoteForId = autoId
                        };

                        db.VoteLogs.Add(vm);

                        db.SaveChanges();

                      
                        HttpCookie cookie = new HttpCookie(url, "true");
                        Response.Cookies.Add(cookie);
                    }
                    break;
                default:
                    break;
            }
            return Json("<br />You rated " + r + " star(s), thanks !");
        }*/
        [HttpGet]
        public ActionResult Search()
        {
            var model = new CompanyViewModel { Company = db.Companies };
            return View(model);
        }
        [HttpPost]
        public ActionResult Search(CompanyViewModel model)
        {
            if (!string.IsNullOrEmpty(model.text))
            {
                model.Company = db.Companies.FullTextSearchQuery(model.text);
            }
            else
            {
                model.Company = db.Companies;
            }
            return View(model);
        }

        #endregion
    }
}