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
        //GET: Posts
        public ActionResult InformalPostView()
        {
            var db = new ApplicationDbContext();
            //var typ = db.Categories.Where(c => c.Type == "informal").Select();
            var model = new PostListViewModel();
            var listCat = db.Categories.Where(p => p.Type == "informal").ToList();
            var listPost = db.Posts.ToList();
            
            foreach(var post in listPost)
            {
                for(int i = 0; i < listCat.Count; i++)
                {
                    if (post.CategoryId == listCat[i].Id)
                    {
                        model.PostList.Add(post);
                        break;
                    }
                }
            }

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
            var model = new PostListViewModel(); /*{ PostList = db.Posts.Where(p => p.Type == "formal").OrderByDescending(p => p.PostTime).ToList() };*/
            var listCat = db.Categories.Where(p => p.Type == "formal").ToList();
            var listPost = db.Posts.ToList();

            foreach (var post in listPost)
            {
                for (int i = 0; i < listCat.Count; i++)
                {
                    if (post.CategoryId == listCat[i].Id)
                    {
                        model.PostList.Add(post);
                        
                    }
                }
            }

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
            newPost.Title = post.Title;
            newPost.Content = post.Content;
            newPost.PostTime = DateTime.Now;
            newPost.AuthorFirstName = user.FirstName;
            newPost.AuthorLastName = user.LastName;
            newPost.AuthorId = user;
            newPost.CategoryId = post.CategoryId;
            


            db.Posts.Add(newPost);    
            db.SaveChanges();
            
            if(post.CategoryId == 1) { return RedirectToAction("InformalPostView", "Posts"); }
            else
            {
                return RedirectToAction("FormalPostView", "Posts");
            }

        }
    }

}