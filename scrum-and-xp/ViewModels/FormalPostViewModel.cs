using scrum_and_xp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace scrum_and_xp.ViewModels
{
    public class FormalPostViewModel
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime PostTime { get; set; }
        public ApplicationUser AuthorId { get; set; }
        public FormalCategory FormalCategory { get; set; }
        public FormalType FormalType { get; set; }
        public List<FormalPost> FormalPosts { get; set; }
        public List<FormalType> FormalTypes { get; set; }
    }
}