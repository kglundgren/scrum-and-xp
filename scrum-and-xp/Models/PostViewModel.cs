using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace scrum_and_xp.Models
{
    public class PostListViewModel
    {
        public PostListViewModel()
        {
            PostList = new List<Post>();
        }
        public List<Post> PostList { get; set; }
        
    }

    

    public class NewPostViewModel
    {
        [Required(ErrorMessage ="Title must be between 1-100 characters.")]
        [MinLength(1)]
        [MaxLength(100)]
        public string Title { get; set; }
        
        public int CategoryId { get; set; }

        [Required(ErrorMessage ="Content must be atleast 1 character long.")]
        [MinLength(1)]
        public string Content { get; set; }
        
    }
    
}