using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace scrum_and_xp.Models
{
    public class InformalCategory
    {
        public int Id { get; set; }
        [MinLength(3)]
        [MaxLength(25)]
        public string Name { get; set; }
    }

    public class CategorySubViewModel
    {
        public List<Category> sub { get; set; }
    }
}