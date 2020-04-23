using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace scrum_and_xp.ViewModels
{
    public class UserRoleViewModel
    {
        public string UserId { get; set; }

        public string UserName { get; set; }

        public bool IsSelected { get; set; }

        public string RoleId { get; set; }

    }
}