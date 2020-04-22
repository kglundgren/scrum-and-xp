using System;

namespace scrum_and_xp.Models
{
    public class SchedulerEvent
    {

        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ApplicationUser Creator { get; set;}
    }
}