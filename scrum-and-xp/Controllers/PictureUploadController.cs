using scrum_and_xp.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace scrum_and_xp.Controllers
{
    public class PictureUploadController : Controller
    {
        private ApplicationUserManager _userManager;

        // GET: FileUpload
        public ActionResult Index()
        {
            var u = User.Identity.Name;

            if (u == "" || u == null)
            {
                return RedirectToAction("Index", "Home");
            }

            else
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult UploadPictures(HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    //Method 2 Get file details from HttpPostedFileBase class    

                    if (file != null)
                    {

                        var u = User.Identity.Name;

                        ApplicationUser applicationUser = UserManager.Users.Where(user => user.Email == u).First();

                        string p = applicationUser.Id;
                        string path = Path.Combine(Server.MapPath("~/Images"), p);
                        file.SaveAs(path + ".jpg");
                        applicationUser.Img = 1;

                        IdentityResult result = UserManager.Update(applicationUser);
                    }
                    ViewBag.FileStatus = "File uploaded successfully.";
                }
                catch (Exception)
                {
                    ViewBag.FileStatus = "Error while file uploading."; ;
                }
            }
            return View("Index");
        }
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
    }
}