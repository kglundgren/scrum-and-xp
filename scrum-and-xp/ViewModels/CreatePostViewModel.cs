using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace scrum_and_xp.ViewModels
{
    public class CreatePostViewModel
    {
        public string Type { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int SelectedInformalCategoryId { get; set; }
        public SelectList InformalCategories { get; set; }
        public int SelectedFormalCategoryId { get; set; }
        public SelectList FormalCategories { get; set; }
        public int SelectedFormalType { get; set; }
        public SelectList FormalTypes { get; set; }
    }
}