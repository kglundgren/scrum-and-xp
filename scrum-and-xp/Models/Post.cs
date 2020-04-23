using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace scrum_and_xp.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime PostTime { get; set; }
        public ApplicationUser AuthorId { get; set; }

        public int CategoryId { get; set; }
        
    }

    public class InformalPost : Post
    {
        public InformalPost()
        {
            InformalCategories = new List<InformalCategory>();
        }
        public virtual ICollection<InformalCategory> InformalCategories { get; set; }
    }
    public class FormalPost : Post
    {
        public FormalPost()
        {
            FormalCategories = new List<FormalCategory>();
        }
        public virtual ICollection<FormalCategory> FormalCategories { get; set; }
    }
}