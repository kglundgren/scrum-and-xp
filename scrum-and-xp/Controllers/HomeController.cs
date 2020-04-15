using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using scrum_and_xp.Models;

namespace scrum_and_xp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var viewModel = new UserIndexViewModel
                {
                    User = ctx.Users.ToList()
                };
                return View(viewModel);
            }
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
    }
}