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

            var infList = db.InformalCategories.ToList();
            var formList = db.FormalCategories.ToList();
            bool exists = false;
            if (cat.Type == "informal")
            {
                for(int i = 0; i < infList.Count; i++)
                {
                    if (infList[i].Name.Equals(cat.Name))
                    {
                        exists = true;
                        break;
                    }
                }
                if(exists == false) 
                {
                    var inf = new InformalCategory() { Name = cat.Name };
                    db.InformalCategories.Add(inf);
                }
            }
            else
            {
                for (int i = 0; i < formList.Count; i++)
                {
                    if (formList[i].Name.Equals(cat.Name))
                    {
                        exists = true;
                        break;
                    }
                }
                if (exists == false)
                {
                    var form = new FormalCategory() { Name = cat.Name };
                    db.FormalCategories.Add(form);
                }
            }
            if(exists == false)
            {
                TempData["notice"] = "A new category has been created.";
            }
            else
            {
                TempData["notice"] = "A category with the submitted name already exists for that type.";
            }
            db.SaveChanges();
            return View();
        }
    }
}