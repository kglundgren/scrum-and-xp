using scrum_and_xp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace scrum_and_xp.ViewModels
{
    public class CreatePostViewModel
    {
        public string Type { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [MinLength(3, ErrorMessage = "Title must be at least 3 characters.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Content is required.")]
        [MinLength(20, ErrorMessage = "Content must be at least 20 characters.")]
        public string Content { get; set; }

        [Required(ErrorMessage = "Must select category.")]
        public int? SelectedCategoryId { get; set; }

        public int SelectedFormalTypeId { get; set; }


        [DataType(DataType.Upload)]
        [Display(Name = "Upload")]
        [Required(ErrorMessage = "Please choose file to upload.")]

        public string ImagePath { get; set; }

        public List<FormalType> FormalTypes { get; set; }
        public List<FormalCategory> FormalCategories { get; set; }
        public List<InformalCategory> InformalCategories { get; set; }
    }
}