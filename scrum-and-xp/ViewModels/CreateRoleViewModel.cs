using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace scrum_and_xp.ViewModels
{
    public class CreateRoleViewModel
    {

        [Required]
        public string Name { get; set; }
    }
}