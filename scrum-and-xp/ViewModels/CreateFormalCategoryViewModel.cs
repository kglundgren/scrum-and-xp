using scrum_and_xp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace scrum_and_xp.ViewModels
{
    public class CreateFormalCategoryViewModel
    {
        [Required(ErrorMessage ="Must enter a name for the category.")]
        public string Name { get; set; }
        public List<FormalType> FormalTypes { get; set; }
        [Required(ErrorMessage = "Must select type")]
        public int? SelectedFormalTypeId { get; set; }
    }
}