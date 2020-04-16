using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace scrum_and_xp.Models
{
    public class Post
    {
        //public Post() { }

        //protected Post(Post post)
        //{
        //    Id = post.Id;
        //    Title = post.Title;
        //    Content = post.Content;
        //    PostTime = post.PostTime;
        //    AuthorFirstName = post.AuthorFirstName;
        //    AuthorLastName = post.AuthorLastName;
        //    AuthorId = post.AuthorId;
        //    Type = post.Type;
        //}

        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime PostTime { get; set; }
        public string AuthorFirstName { get; set; }
        public string AuthorLastName { get; set; }
        public ApplicationUser AuthorId { get; set; }

        public int CategoryId { get; set; }
        
    }

    //public class InformalPost : Post
    //{
    //    public InformalPost() { }
    //    public InformalPost(Post post) : base(post) { }
    //    public  InformalCategory InformalCat { get; set; }
    //}
    //public class FormalPost : Post
    //{
    //    public FormalPost() { }
    //    public FormalPost(Post post) : base(post) { }
    //    public FormalCategory FormalCat { get; set; }
    //}
}