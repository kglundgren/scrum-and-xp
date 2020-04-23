using scrum_and_xp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace scrum_and_xp.ViewModels
{
    public class CurrentUsersOnline
    {
        public string CurrentUserName { get; set; }

        public List<ApplicationUser> loggedInUsers { get; set; }
    }
}