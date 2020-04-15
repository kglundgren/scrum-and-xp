using Microsoft.AspNet.Identity;
using scrum_and_xp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace scrum_and_xp.Controllers
{
    public class PostsController : Controller
    {
        // GET: Posts
        public ActionResult InformalPostView()
        {
            var db = new ApplicationDbContext();
            var model = new InformalPostListViewModel() { InformalPostList = db.InformalPosts.OrderByDescending(p => p.PostTime).ToList() };

            if (model != null)
            {
                return View(model);
            }
            else
            {
                return View();
            }
        }
        public ActionResult FormalPostView()
        {
            var db = new ApplicationDbContext();
            var model = new FormalPostListViewModel() { FormalPostList = db.FormalPosts.OrderByDescending(p => p.PostTime).ToList() };

            if (model != null)
            {
                return View(model);
            }
            else
            {
                return View();
            }
        }

        public ActionResult CreatePostView()
        {
            return View();

        }


        [HttpPost]
        public ActionResult CreatePostView(NewPostViewModel post)
        {

            var db = new ApplicationDbContext();
            var userId = User.Identity.GetUserId();
            var user = db.Users.FirstOrDefault(a => a.Id == userId);

            var newPost = new Post();
            newPost.Title = newPost.Title;
            newPost.Content = newPost.Content;
            newPost.PostTime = DateTime.Now;
            newPost.AuthorFirstName = user.FirstName;
            newPost.AuthorLastName = user.LastName;
            newPost.AuthorId = user;

            if (post.Type == "informal")
            {
                var infPost = new InformalPost(newPost);
                db.InformalPosts.Add(infPost);
            }
            else
            {
                var formPost = new FormalPost(newPost);
                db.FormalPosts.Add(formPost);
            }
                db.SaveChanges();
            
            if(post.Type == "informal") { return RedirectToAction("InformalPostView", "Posts"); }
            else
            {
                return RedirectToAction("FormalPostView", "Posts");
            }

        }
    }

}