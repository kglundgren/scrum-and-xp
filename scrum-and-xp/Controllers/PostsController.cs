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
            var model = new InformalPostListViewModel() { InformalPostList = db.InformalPosts.OrderByDescending(p => p.postTime).ToList() };

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
            var model = new FormalPostListViewModel() { FormalPostList = db.FormalPosts.OrderByDescending(p => p.postTime).ToList() };

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
            var _infPost = new InformalPost();
            var _formPost = new FormalPost();

            if (post.Type == "informal") 
            {
                _infPost.Title = post.Title;
                _infPost.Content = post.Content;
                _infPost.postTime = DateTime.Now;
                _infPost.AuthorFirstName = user.FirstName;
                _infPost.AuthorLastName = user.LastName;
                _infPost.AuthorId = user;
                db.InformalPosts.Add(_infPost);
            }

            else
            {
                _formPost.Title = post.Title;
                _formPost.Content = post.Content;
                _formPost.postTime = DateTime.Now;
                _formPost.AuthorFirstName = user.FirstName;
                _formPost.AuthorLastName = user.LastName;
                _formPost.AuthorId = user;
                db.FormalPosts.Add(_formPost);
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