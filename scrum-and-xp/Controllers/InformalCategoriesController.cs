using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using scrum_and_xp.Models;

namespace scrum_and_xp.Controllers
{
    public class InformalCategoriesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: InformalCategories
        public ActionResult Index()
        {
            return View(db.InformalCategories.ToList());
        }

        // GET: InformalCategories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InformalCategory informalCategory = db.InformalCategories.Find(id);
            if (informalCategory == null)
            {
                return HttpNotFound();
            }
            return View(informalCategory);
        }

        // GET: InformalCategories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: InformalCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] InformalCategory informalCategory)
        {
            if (ModelState.IsValid)
            {
                db.InformalCategories.Add(informalCategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(informalCategory);
        }

        // GET: InformalCategories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InformalCategory informalCategory = db.InformalCategories.Find(id);
            if (informalCategory == null)
            {
                return HttpNotFound();
            }
            return View(informalCategory);
        }

        // POST: InformalCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] InformalCategory informalCategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(informalCategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(informalCategory);
        }

        // GET: InformalCategories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InformalCategory informalCategory = db.InformalCategories.Find(id);
            if (informalCategory == null)
            {
                return HttpNotFound();
            }
            return View(informalCategory);
        }

        // POST: InformalCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            InformalCategory informalCategory = db.InformalCategories.Find(id);
            db.InformalCategories.Remove(informalCategory);
            db.SaveChanges();
            return RedirectToAction("Index");
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
