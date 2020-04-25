using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json;
using scrum_and_xp.Models;
using scrum_and_xp.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace scrum_and_xp.Controllers
{
    [Authorize(Roles = "Users,Admin")]
    public class PostsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
        private readonly RoleManager<IdentityRole> RoleManager;
        private readonly ApplicationUserManager UserManager;

        public PostsController()
        {
            RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            UserManager = new ApplicationUserManager(new UserStore<ApplicationUser>(db));
        }
        
        // GET: Posts
        public ActionResult InformalPosts()
        {
            var model = new InformalPostViewModel();
            model.InformalPosts = db.InformalPosts.Include("InformalCategories").Include("AuthorId").OrderByDescending(x => x.PostTime).ToList();
            model.InformalCategories = new SelectList(db.InformalCategories, "Id", "Name");
            return View(model);
        }
        // GET: Posts
        public ActionResult FormalPosts()
        {
            var model = new FormalPostViewModel();
            model.FormalPosts = db.FormalPosts.Include("FormalCategories").Include("AuthorId").OrderByDescending(x => x.PostTime).ToList();
            model.FormalCategories = new SelectList(db.FormalCategories, "Id", "Name");
            model.FormalTypes = new SelectList(db.FormalTypes, "Id", "Name");
            return View(model);
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
                model.FormalTypes = db.FormalTypes.ToList();
            }
            else if (type.Equals("Informal"))
            {
                model.Type = "Informal";
                model.InformalCategories = db.InformalCategories.ToList();
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
            if (model.SelectedCategoryId is null)
            {
                if (ModelState.ContainsKey("SelectedCategoryId"))
                {
                    ModelState["SelectedCategoryId"].Errors.Clear();
                    ModelState.AddModelError("SelectedCategoryId", "Must select category.");
                }
            }

            var authorId = db.Users.Find(User.Identity.GetUserId());
            model.FormalTypes = db.FormalTypes.ToList();
            model.FormalCategories = db.FormalCategories.ToList();
            model.InformalCategories = db.InformalCategories.ToList();
            if (model.Type == "Formal")
            {
                var formPost = new FormalPost
                {
                    Title = model.Title,
                    Content = model.Content,
                    PostTime = DateTime.Now,
                    AuthorId = authorId
                };
                var formCat = db.FormalCategories.FirstOrDefault(cat => cat.Id == model.SelectedCategoryId);
                formPost.FormalCategories.Add(formCat);

                if (ModelState.IsValid)
                {
                    db.FormalPosts.Add(formPost);
                    db.SaveChanges();
                    return RedirectToAction("FormalPosts");
                }
                return View(model);
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
                var infCategory = db.InformalCategories.FirstOrDefault(cat => cat.Id == model.SelectedCategoryId);
                infPost.InformalCategories.Add(infCategory);

                if (ModelState.IsValid)
                {
                    db.InformalPosts.Add(infPost);
                    db.SaveChanges();
                    return RedirectToAction("InformalPosts");
                }
                return View(model);
            }

            return RedirectToAction("Index");
        }

        // GET: Posts/FillCategory
        public ActionResult FillCategory(int? type)
        {
            var selectListItems = new List<SelectListItem>();
            selectListItems.Add(new SelectListItem() { Value = "null", Text = "Please select formal category" });
            if (type != null)
            {
                var categories = new List<FormalCategory>();
                categories = db.FormalCategories.Include("Type").Where(c => c.Type.Id == type).ToList();
                foreach (var item in categories)
                {
                    selectListItems.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.Name });
                }
                return Json(selectListItems, JsonRequestBehavior.AllowGet);
            }
            // If the type is null, return only default options.
            return Json(selectListItems, JsonRequestBehavior.AllowGet);
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

        // GET: Posts/FilterPostsOnType
        public ActionResult FilterPostsOnType(int type)
        {
            var posts = db.FormalPosts.Include("AuthorId").Include("FormalCategories.Type")
                .Where(p => p.FormalCategories.Any(c => c.Type.Id == type))
                .OrderByDescending(x => x.PostTime)
                .ToArray();
            var json = JsonConvert.SerializeObject(posts, jsonSerializerSettings);
            return Content(json, "application/json");
        }

        // GET: Posts/FilterFormalPosts
        public ActionResult FilterFormalPosts(int category)
        {
            List<FormalPost> posts = new List<FormalPost>();
            if (category == 0)
            {
                posts = db.FormalPosts.Include("AuthorId").Include("FormalCategories.Type").ToList();
            }
            else
            {
                posts = db.FormalPosts.Include("AuthorId").Include("FormalCategories.Type")
                    .Where(p => p.FormalCategories.Any(c => c.Id == category))
                    .OrderByDescending(x => x.PostTime)
                    .ToList();
            }
            var json = JsonConvert.SerializeObject(posts, jsonSerializerSettings);
            return Content(json, "application/json");
        }

        //GET: Posts/Edit/5
        public async Task<ActionResult> Edit(int? id, string typeId)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var post = new PostViewModel();
            var formal = db.FormalPosts.Find(id);
            var userId = User.Identity.GetUserId();
            var user = db.Users.FirstOrDefault(c => c.Id == userId);
            var role = await RoleManager.FindByNameAsync("Admin");
            var admin = await UserManager.IsInRoleAsync(userId, role.Name);

            if (formal != null && typeId == "formal")
            {
                if (formal.AuthorId == user || admin)
                {
                    post.Id = formal.Id;
                    post.Title = formal.Title;
                    post.Content = formal.Content;
                    post.PostTime = formal.PostTime;
                    post.AuthorId = formal.AuthorId;
                    post.Formal = true;
                    return View(post);
                }
            }
            else
            {
                var informal = db.InformalPosts.Find(id);
                if (informal != null)
                {
                    if (formal.AuthorId == user || admin)
                    {
                        post.Id = informal.Id;
                        post.Title = informal.Title;
                        post.Content = informal.Content;
                        post.PostTime = informal.PostTime;
                        post.AuthorId = informal.AuthorId;
                        post.Formal = false;
                        return View(post);
                    }

                }
            }
            ViewBag.Auth = "Authorization to edit post not granted.";
            return RedirectToAction("Index", "Home");
        }

        //POST: Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Content,PostTime,Formal")] PostViewModel post)
        {
            if (ModelState.IsValid)
            {
                if (post.Formal)
                {
                    var formal = db.FormalPosts.FirstOrDefault(p => p.Id == post.Id);
                    formal.Title = post.Title;
                    formal.Content = post.Content;
                    formal.PostTime = DateTime.Now;
                    db.SaveChanges();
                    return RedirectToAction("InformalPost");
                }
                else
                {
                    var informal = db.InformalPosts.FirstOrDefault(p => p.Id == post.Id);
                    informal.Title = post.Title;
                    informal.Content = post.Content;
                    informal.PostTime = DateTime.Now;
                    db.SaveChanges();
                    return RedirectToAction("InformalPost");
                }
                
                
            }
            return View(post);
        }
        public async Task<ActionResult> Delete(int? id, string typeId)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var post = new PostViewModel();
            var formal = db.FormalPosts.Find(id);
            var userId = User.Identity.GetUserId();
            var user = db.Users.FirstOrDefault(c => c.Id == userId);
            var role = await RoleManager.FindByNameAsync("Admin");
            var admin = await UserManager.IsInRoleAsync(userId, role.Name);

            if (formal != null && typeId == "formal")
            {
                if (formal.AuthorId == user || admin)
                {
                    post.Id = formal.Id;
                    post.Title = formal.Title;
                    post.Content = formal.Content;
                    post.PostTime = formal.PostTime;
                    post.AuthorId = formal.AuthorId;
                    post.Formal = true;
                    return View(post);
                }
            }
            else
            {
                var informal = db.InformalPosts.Find(id);
                if (informal != null)
                {
                    if (informal.AuthorId == user || admin)
                    {
                        post.Id = informal.Id;
                        post.Title = informal.Title;
                        post.Content = informal.Content;
                        post.PostTime = informal.PostTime;
                        post.AuthorId = informal.AuthorId;
                        post.Formal = false;
                        return View(post);
                    }

                }
            }
            ViewBag.Auth = "Authorization to edit post not granted.";
            return RedirectToAction("Index", "Home");
        }


        [HttpPost]
        public ActionResult Delete([Bind(Include = "Id,Formal")]PostViewModel post)
        {
            if (post != null)
            {
                if (post.Formal)
                {
                    var formal = db.FormalPosts.FirstOrDefault(c => c.Id == post.Id);
                    db.FormalPosts.Remove(formal);
                    db.SaveChanges();
                    return RedirectToAction("FormalPosts");
                }
                else
                {
                    var informal = db.InformalPosts.FirstOrDefault(c => c.Id == post.Id);
                    db.InformalPosts.Remove(informal);
                    db.SaveChanges();
                    return RedirectToAction("InformalPosts");
                }
            }
            return View();
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
