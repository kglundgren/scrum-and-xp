using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace scrum_and_xp.Models
{
    public class UpcomingMeeting
    {
        public int Id { get; set; }       
        public string Description { get; set; }
        public DateTime? Option1 { get; set; }
        public DateTime? Option2 { get; set; }
        public DateTime? Option3 { get; set; }
        public TimeSpan Duration { get; set; }
        public int TotalAnswered { get; set; }
        public ApplicationUser Author { get; set; }

    }
}