using scrum_and_xp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace scrum_and_xp.ViewModels
{
    public class PostViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Title is required.")]
        [MinLength(3, ErrorMessage = "Title must be at least 3 characters.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Content is required.")]
        [MinLength(20, ErrorMessage = "Content must be at least 20 characters.")]
        public string Content { get; set; }
        public DateTime PostTime { get; set; }
        public ApplicationUser AuthorId { get; set; }
        public bool Formal { get; set; }
    }
}