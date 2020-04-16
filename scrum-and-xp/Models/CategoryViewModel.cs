using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace scrum_and_xp.Models
{
    public class CategoryViewModel
    {
<<<<<<< HEAD:scrum-and-xp/Models/FormalCategory.cs
        public int Id { get; set; }
        [MinLength(3)]
        [MaxLength(25)]
=======
>>>>>>> master:scrum-and-xp/Models/CategoryViewModel.cs
        public string Name { get; set; }
        public string Type { get; set; }
        
    }
}