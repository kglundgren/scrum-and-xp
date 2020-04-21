using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace scrum_and_xp.ViewModels
{
    public class CreateInformalCategoryViewModel
    {
        [Required(ErrorMessage = "Name is required.")]
        [MinLength(3, ErrorMessage = "Must be at least 3 characters.")]
        public string Name { get; set; }
    }
}