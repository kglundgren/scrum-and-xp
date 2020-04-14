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
        public DateTime postTime { get; set; }
        public string AuthorFirstName { get; set; }
        public string AuthorLastName { get; set; }

        public ApplicationUser AuthorId { get; set; }
    }

    public class InformalPost : Post
    {
        public  InformalCategory Category { get; set; }
    }
    public class FormalPost : Post
    {
        public FormalCategory Category { get; set; }
    }
}