using scrum_and_xp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace scrum_and_xp.ViewModels
{
    public class CreateFormalCategoryViewModel
    {
        public string CategoryName { get; set; }
        public int SelectedFormalTypeId { get; set; }
        public SelectList FormalTypes { get; set; }
    }
}