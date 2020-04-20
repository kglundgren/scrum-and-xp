using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using scrum_and_xp.Models;
using scrum_and_xp.ViewModels;
using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Web.Mvc;

namespace scrum_and_xp.Controllers
{
    [Authorize]
    public class PostsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };

        // GET: Posts
        public ActionResult InformalPosts()
        {
            var model = new InformalPostViewModel();
            model.InformalPosts = db.InformalPosts.Include("InformalCategories").Include("AuthorId").ToList();
            model.InformalCategories = new SelectList(db.InformalCategories, "Id", "Name");
            return View(model);
        }
        // GET: Posts
        public ActionResult FormalPosts()
        {
            return View(db.FormalPosts.Include("FormalCategories.Type").Include("AuthorId").ToList());
        }

        //// GET: Posts/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Post post = db.Posts.Find(id);
        //    if (post == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(post);
        //}

        // GET: Posts/Create
        public ActionResult Create(string type)
        {
            var model = new CreatePostViewModel();
            if (type is null || type.Equals("Formal"))
            {
                model.Type = "Formal";
                model.FormalCategories = new SelectList(db.FormalCategories, "Id", "Name");
                model.FormalTypes = new SelectList(db.FormalTypes, "Id", "Name");
            }
            else if (type.Equals("Informal"))
            {
                model.Type = "Informal";
                model.InformalCategories = new SelectList(db.InformalCategories, "Id", "Name");
            }

            return View(model);
        }

        // POST: Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,Title,Content,PostTime")] Post post)
        public ActionResult Create(CreatePostViewModel model)
        {
            var authorId = db.Users.Find(User.Identity.GetUserId());
            if (model.Type == "Formal")
            {
                var formPost = new FormalPost
                {
                    Title = model.Title,
                    Content = model.Content,
                    PostTime = DateTime.Now,
                    AuthorId = authorId
                };
                var formCat = db.FormalCategories.FirstOrDefault(cat => cat.Id == model.SelectedFormalCategoryId);
                formPost.FormalCategories.Add(formCat);

                if (ModelState.IsValid)
                {
                    db.FormalPosts.Add(formPost);
                    db.SaveChanges();
                    return RedirectToAction("FormalPosts");
                }
            }
            else if (model.Type == "Informal")
            {
                var infPost = new InformalPost
                {
                    Title = model.Title,
                    Content = model.Content,
                    PostTime = DateTime.Now,
                    AuthorId = authorId
                };
                var infCategory = db.InformalCategories.FirstOrDefault(cat => cat.Id == model.SelectedInformalCategoryId);

                infPost.InformalCategories.Add(infCategory);

                if (ModelState.IsValid)
                {
                    db.InformalPosts.Add(infPost);
                    db.SaveChanges();
                    return RedirectToAction("InformalPosts");
                }
            }


            return RedirectToAction("Index");
        }

        // GET: Posts/FillCategory
        public ActionResult FillCategory(int type)
        {
            var categories = db.FormalCategories.Include("Type").Where(c => c.Type.Id == type);
            return Json(new SelectList(categories.ToArray(), "Id", "Name"), JsonRequestBehavior.AllowGet);
        }

        // GET: Posts/FilterInformalPosts
        public ActionResult FilterInformalPosts(int category)
        {
            var posts = db.InformalPosts.Include("AuthorId")
                .Where(p => p.InformalCategories.Any(c => c.Id == category))
                .OrderByDescending(x => x.PostTime)
                .ToArray();
            var json = JsonConvert.SerializeObject(posts, jsonSerializerSettings);
            return Content(json, "application/json");
        }

        // GET: Posts/FilterFormalPosts
        public ActionResult FilterFormalPosts(int category)
        {
            var posts = db.FormalPosts.Include("AuthorId")
                .Where(p => p.FormalCategories.Any(c => c.Id == category))
                .OrderByDescending(x => x.PostTime)
                .ToArray();
            var json = JsonConvert.SerializeObject(posts, jsonSerializerSettings);
            return Content(json, "application/json");
        }

        //// GET: Posts/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Post post = db.Posts.Find(id);
        //    if (post == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(post);
        //}

        // POST: Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Content,PostTime")] Post post)
        {
            if (ModelState.IsValid)
            {
                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(post);
        }

        //// GET: Posts/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Post post = db.Posts.Find(id);
        //    if (post == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(post);
        //}

        //// POST: Posts/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Post post = db.Posts.Find(id);
        //    db.Posts.Remove(post);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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
