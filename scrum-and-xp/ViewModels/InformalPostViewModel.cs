using scrum_and_xp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace scrum_and_xp.ViewModels
{
    public class InformalPostViewModel
    {
        public int SelectedCategoryId { get; set; }
        public List<InformalPost> InformalPosts { get; set; }
        public SelectList InformalCategories { get; set; }
    }
}