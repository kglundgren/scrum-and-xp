using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace scrum_and_xp.ViewModels
{
    public class EditRoleViewModel
    {
        public string Id { get; set; }

        public string RoleName { get; set; }

        public List<string> Users { get; set; }

        public EditRoleViewModel()
        {
            Users = new List<string>();
        }


    }
}