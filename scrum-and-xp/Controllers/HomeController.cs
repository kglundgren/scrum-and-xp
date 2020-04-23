using Microsoft.AspNet.Identity;
using scrum_and_xp.Models;
using scrum_and_xp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace scrum_and_xp.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Chat()
        {
            var user = db.Users.Find(User.Identity.GetUserId());
            var model = new Chat()
            {
                Name = user.FirstName + " " + user.LastName
            };
            return View(model);
        }

        //
        // GET: /Current list of users online

        public ActionResult Chatters()
        {
            var user = db.Users.Find(User.Identity.GetUserId());
            var model = new CurrentUsersOnline()
            {
                CurrentUserName = user.FirstName + "" + user.LastName
            };
            if (ModelState.IsValid)
            {                
                if (user != null)
                {
                    if (HttpRuntime.Cache["LoggedInUsers"] != null) //if the list exists, add this user to it
                    {
                        //get the list of logged in users from the cache
                        model.loggedInUsers = (List<ApplicationUser>)HttpRuntime.Cache["LoggedInUsers"];
                        //add this user to the list
                        model.loggedInUsers.Add(user);
                        //add the list back into the cache
                        HttpRuntime.Cache["LoggedInUsers"] = model.loggedInUsers;
                    }
                    else //the list does not exist so create it
                    {
                        //create a new list
                        model.loggedInUsers = new List<ApplicationUser>();
                        //add this user to the list
                        model.loggedInUsers.Add(user);
                        //add the list into the cache
                        HttpRuntime.Cache["LoggedInUsers"] = model.loggedInUsers;
                    }
                }
            }            
            return View(model);
        }


        public ActionResult CurrentUserLogOff()
        {
            var user = db.Users.Find(User.Identity.GetUserName()); //get the users username who is logged in
            if (HttpRuntime.Cache["LoggedInUsers"] != null)//check if the list has been created
            {
                //the list is not null so we retrieve it from the cache
                List<ApplicationUser> loggedInUsers = (List<ApplicationUser>)HttpRuntime.Cache["LoggedInUsers"];
                if (loggedInUsers.Contains(user))//if the user is in the list
                {
                    //then remove them
                    loggedInUsers.Remove(user);
                }
                // else do nothing
            }
            //else do nothing
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}
