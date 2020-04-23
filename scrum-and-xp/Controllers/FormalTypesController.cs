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
    [Authorize(Roles = "Users")]
    public class FormalTypesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: FormalTypes
        public ActionResult Index()
        {
            return View(db.FormalTypes.ToList());
        }

        // GET: FormalTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FormalType formalType = db.FormalTypes.Find(id);
            if (formalType == null)
            {
                return HttpNotFound();
            }
            return View(formalType);
        }

        // GET: FormalTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FormalTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] FormalType formalType)
        {
            if (ModelState.IsValid)
            {
                db.FormalTypes.Add(formalType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(formalType);
        }

        // GET: FormalTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FormalType formalType = db.FormalTypes.Find(id);
            if (formalType == null)
            {
                return HttpNotFound();
            }
            return View(formalType);
        }

        // POST: FormalTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] FormalType formalType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(formalType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(formalType);
        }

        // GET: FormalTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FormalType formalType = db.FormalTypes.Find(id);
            if (formalType == null)
            {
                return HttpNotFound();
            }
            return View(formalType);
        }

        // POST: FormalTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FormalType formalType = db.FormalTypes.Find(id);
            db.FormalTypes.Remove(formalType);
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
