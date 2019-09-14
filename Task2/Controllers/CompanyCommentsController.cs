using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Task2.Models;

namespace StarRatingSystem.Controllers
{
    public class ArticlesCommentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CompanyComments
        public ActionResult Index()
        {
            return View(db.CompanyComments.ToList());
        }

        // GET: CompanyComments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyComment companyComment = db.CompanyComments.Find(id);
            if (companyComment == null)
            {
                return HttpNotFound();
            }
            return View(companyComment);
        }

        // GET: CompanyComments/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(FormCollection form)
        {
            var comment = form["Comment"].ToString();
            var companyId = int.Parse(form["CompanyId"]);
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

        // POST: CompanyComments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CommentId,Comments,ThisDateTime,CompanyId,Rating")] CompanyComment articlesComment)
        {
            if (ModelState.IsValid)
            {
                db.CompanyComments.Add(articlesComment);
                db.SaveChanges();
                return RedirectToAction("Details", "Manage");
            }

            return View(articlesComment);
        }

        // GET: CompanyComments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyComment companyComment = db.CompanyComments.Find(id);
            if (companyComment == null)
            {
                return HttpNotFound();
            }
            return View(companyComment);
        }

        // POST: CompanyComments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CommentId,Comments,ThisDateTime,CompanyId,Rating")] CompanyComment companyComment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(companyComment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details","Manage");
            }
            return View(companyComment);
        }

        // GET: CompanyComments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyComment companyComment = db.CompanyComments.Find(id);
            if (companyComment == null)
            {
                return HttpNotFound();
            }
            return View(companyComment);
        }

        // POST: CompanyComments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CompanyComment companyComment = db.CompanyComments.Find(id);
            db.CompanyComments.Remove(companyComment);
            db.SaveChanges();
            return RedirectToAction("Details", "Manage");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
