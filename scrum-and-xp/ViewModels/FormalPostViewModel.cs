using scrum_and_xp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace scrum_and_xp.ViewModels
{
    public class FormalPostViewModel
    {
        public int SelectedTypeId { get; set; }
        public int SelectedCategoryId { get; set; }
        public List<FormalPost> FormalPosts { get; set; }
        public SelectList FormalCategories { get; set; }
        public SelectList FormalTypes { get; set; }
    }
}