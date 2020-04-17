using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace scrum_and_xp.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class FormalType
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class FormalCategory : Category
    {
        public FormalType Type { get; set; }
        public virtual ICollection<FormalPost> FormalPosts { get; set; }
    }

    public class InformalCategory : Category
    {
        public virtual ICollection<InformalPost> InformalPosts { get; set; }
    }
}