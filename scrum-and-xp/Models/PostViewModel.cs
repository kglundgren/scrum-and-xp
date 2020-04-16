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

    public class PostListViewModel 
    {
        public List<Post> PostList { get; set; }
    }
    
}