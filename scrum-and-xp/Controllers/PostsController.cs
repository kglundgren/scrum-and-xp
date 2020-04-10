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
            var model = new PostListViewModel() { PostList = db.Posts.OrderByDescending(p => p.postTime).ToList() };

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
        public ActionResult CreatePostView(PostViewModel post)
        {

            var db = new ApplicationDbContext();
            var userId = User.Identity.GetUserId();
            var user = db.Users.FirstOrDefault(a => a.Id == userId);
            var _post = new Post();
            _post.Title = post.Title;
            _post.Content = post.Content;
            _post.postTime = DateTime.Now;
            _post.AuthorId = user;

                db.Posts.Add(_post);
                db.SaveChanges();
            

            return View();
        }
    }
   
}