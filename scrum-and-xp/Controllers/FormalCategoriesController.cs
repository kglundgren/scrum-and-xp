using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using scrum_and_xp.Models;
using scrum_and_xp.ViewModels;

namespace scrum_and_xp.Controllers
{
   
    public class FormalCategoriesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: FormalCategories
        public ActionResult Index()
        {
            return View(db.FormalCategories.Include("Type").ToList());
        }

        // GET: FormalCategories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FormalCategory formalCategory = db.FormalCategories.Find(id);
            if (formalCategory == null)
            {
                return HttpNotFound();
            }
            return View(formalCategory);
        }

        // GET: FormalCategories/Create
        public ActionResult Create()
        {
            var formalTypes = db.FormalTypes.ToList();
            var model = new CreateFormalCategoryViewModel()
            {
                FormalTypes = formalTypes,
            };
            return View(model);
        }

        // POST: FormalCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateFormalCategoryViewModel model)
        {
            model.FormalTypes = db.FormalTypes.ToList();
            var formalCategory = new FormalCategory()
            {
                Name = model.Name,
                Type = model.FormalTypes.FirstOrDefault(type => type.Id == model.SelectedFormalTypeId)
            };

            if (ModelState.IsValid)
            {
                db.FormalCategories.Add(formalCategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: FormalCategories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FormalCategory formalCategory = db.FormalCategories.Find(id);
            if (formalCategory == null)
            {
                return HttpNotFound();
            }
            return View(formalCategory);
        }

        // POST: FormalCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] FormalCategory formalCategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(formalCategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(formalCategory);
        }

        // GET: FormalCategories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FormalCategory formalCategory = db.FormalCategories.Find(id);
            if (formalCategory == null)
            {
                return HttpNotFound();
            }
            return View(formalCategory);
        }

        // POST: FormalCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FormalCategory formalCategory = db.FormalCategories.Find(id);
            db.FormalCategories.Remove(formalCategory);
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
