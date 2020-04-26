using Microsoft.AspNet.Identity;
using scrum_and_xp.Models;
using scrum_and_xp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace scrum_and_xp.Controllers
{
    public class ChatController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            var user = db.Users.Find(User.Identity.GetUserId());
            var model = new ChatUser
            {
                UserName = user.FirstName + " " + user.LastName
            };
            return View(model);
        }
    }
}