using scrum_and_xp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace scrum_and_xp.ViewModels
{
    public class UpcomingMeetingViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="The meeting needs to have a description")]
        public string Description { get; set; }
        [Required(ErrorMessage ="All the time options need to be filled.")]
        public DateTime? Option1 { get; set; }
        [Required(ErrorMessage ="All the time options need to be filled.")]
        public DateTime? Option2 { get; set; }
        [Required(ErrorMessage = "All the time options need to be filled.")]
        public DateTime? Option3 { get; set; }
        [Required(ErrorMessage ="The meeting needs to have a time duration")]
        public TimeSpan Duration { get; set; }
        public string SelectedResponse { get; set; }
        public ApplicationUser Author { get; set; }
    }

    public class MeetingInvitesViewModel
    {
        public List<UpcomingMeetingViewModel> UpcomingMeetingViewModels { get; set; }
        public List<CreatedInviteViewModel> CreatedInvites { get; set; }
    }   

    public class CreatedInviteViewModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime? Option1 { get; set; }
        public DateTime? Option2 { get; set; }
        public DateTime? Option3 { get; set; }
        public int Option1Votes { get; set; }
        public int Option2Votes { get; set; }
        public int Option3Votes { get; set; }
        public int TotalInvited { get; set; }
        public TimeSpan Duration { get; set; }
        public string SelectedTime { get; set; }
        public ApplicationUser Author { get; set; }
    }

}