using scrum_and_xp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace scrum_and_xp.Controllers
{
    public class CategoriesController : Controller
    {
        // GET: Categories
        public ActionResult NewParentCategoryView()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NewParentCategoryView(CategoryViewModel cat)
        {
            var db = new ApplicationDbContext();
            var _cat = new Category()
            {
                Name = cat.Name,
                Type = cat.Type
            
            };


            db.Categories.Add(_cat);
            db.SaveChanges();
            return View();
        }
    }
}