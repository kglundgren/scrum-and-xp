using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace scrum_and_xp.Models
{
    public class UserIndexViewModel : Controller
    {
        public List<ApplicationUser> User { get; set; }
    }
}