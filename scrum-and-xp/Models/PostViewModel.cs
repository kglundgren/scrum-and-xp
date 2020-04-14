using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace scrum_and_xp.Models
{
    public class PostViewModel
    {
        public string Title { get; set; }
        
        public string Content { get; set; }
    }

    public class InformalPostListViewModel 
    {
        public List<InformalPost> InformalPostList { get; set; }
    }
    public class FormalPostListViewModel
    {
        public List<FormalPost> FormalPostList { get; set; }
    }

    public class NewPostViewModel
    {
        [Required(ErrorMessage ="Title must be between 1-100 characters.")]
        [MinLength(1)]
        [MaxLength(100)]
        public string Title { get; set; }
        public string Type { get; set; }

        [Required(ErrorMessage ="Content must be atleast 1 character long.")]
        [MinLength(1)]
        public string Content { get; set; }
    }
    
}