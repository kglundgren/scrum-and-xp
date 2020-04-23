using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using scrum_and_xp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using scrum_and_xp.ViewModels;

namespace scrum_and_xp.Controllers
{

    public class RoleController : Controller
    {
        //private readonly RoleManager<IdentityRole> roleManager;
        private ApplicationDbContext db = new ApplicationDbContext();
        private readonly RoleManager<IdentityRole> RoleManager;
        private readonly ApplicationUserManager UserManager;

        public RoleController()
        {
            RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            UserManager = new ApplicationUserManager(new UserStore<ApplicationUser>(db));
        }
        // GET: Role
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CreateRole()
        {
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> CreateRole(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole
                {
                    Name = model.Name

                };

                IdentityResult result = await RoleManager.CreateAsync(identityRole);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles", "Role");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error);
                }
            }
            return View(model);


        }
        [HttpGet]
        public ActionResult ListRoles()
        {
           
            var roles = RoleManager.Roles;
            
            return View(roles);
        }

        [HttpGet]
        public async Task<ActionResult> EditRole(string id)
        {
            var role = await RoleManager.FindByIdAsync(id);

            if(role == null)
            {
                ViewBag.errormessage = "Error";
                return HttpNotFound();
            }
            var model = new EditRoleViewModel
            {
                Id = role.Id,
                RoleName = role.Name
            };

            foreach(var users in UserManager.Users.ToList())
            {
                if (await UserManager.IsInRoleAsync(users.Id, role.Name))
                {
                    model.Users.Add(users.UserName);
                }
            }
            return View(model);
           
        }

        [HttpPost]
        public async Task<ActionResult> EditRole(EditRoleViewModel model)
        {
            var role = await RoleManager.FindByIdAsync(model.Id);

            if(role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {model.Id} cannot be found";
                return View("NotFound");

            }
            else
            {
                role.Name = model.RoleName;
                var result = await RoleManager.UpdateAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles");
                }
              foreach(var e in result.Errors)
                {
                    return RedirectToAction("ListRoles");
                }

                return View(model);
            }
        }

        [HttpGet]
        public async Task<ActionResult> EditUsersInRole(string roleId)
        {
            var role = await RoleManager.FindByIdAsync(roleId);

            ViewBag.RoleId = roleId;
            ViewBag.RoleName = role.Name;

            if(role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {roleId} cannot be found";
                return HttpNotFound();
            }

            var model = new List<UserRoleViewModel>();

            foreach(var user in UserManager.Users.ToList())
            {
                var UserRoleViewModel = new UserRoleViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    IsSelected = await UserManager.IsInRoleAsync(user.Id, role.Name),
                    RoleId = role.Id
                };

                model.Add(UserRoleViewModel);
            }
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> EditUsersInRole(List<UserRoleViewModel> model , string roleId)
        {
            var role = await RoleManager.FindByIdAsync(roleId);
            

            if(role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {roleId} cannot be found";
                return HttpNotFound();
            }

            for (int i = 0; i < model.Count; i++)
            {
                var userInDb = await UserManager.FindByIdAsync(model[i].UserId);

                IdentityResult result = null;

                // Add the user if he's not in the role and the box is checked.
                if (model[i].IsSelected && !(await UserManager.IsInRoleAsync(userInDb.Id, role.Name)))
                {
                    result = await UserManager.AddToRoleAsync(userInDb.Id, role.Name);
                }
                // Remove the user if he's in the role and the box is unchecked.
                else if (!model[i].IsSelected && await UserManager.IsInRoleAsync(userInDb.Id, role.Name))
                {
                    result = await UserManager.RemoveFromRoleAsync(userInDb.Id, role.Name);
                }
                else
                {
                    continue;
                }

                if (result.Succeeded)
                {
                    if (i < (model.Count - 1))
                        continue;
                    else
                        return RedirectToAction("EditRole", new { Id = roleId });
                }
            }
            return RedirectToAction("EditRole", new { id = roleId });
        }

        [HttpGet]
        public async Task<ActionResult> NewUsers()
        {

            var roles = RoleManager.Roles;
            var role = await RoleManager.FindByNameAsync("Users");
            ViewBag.Role = role.Id;

            var model = new List<NewUserViewModel>();
           

            foreach (var user in UserManager.Users.ToList())
            {


                var NewUserModel = new NewUserViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    IsSelected = await UserManager.IsInRoleAsync(user.Id, role.Name),
                    FirstName = user.FirstName,
                    LastName = user.LastName
                };

                

                if(NewUserModel.IsSelected == false)
                {
                    model.Add(NewUserModel);
                }
                

            }
            
            
            
            if (model != null)
            {
                return View(model);
            }

            return View();
        }



    }   }    
    
