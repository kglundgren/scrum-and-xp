﻿using scrum_and_xp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace scrum_and_xp.ViewModels
{
    public class CreateUpcomingMeetingViewModel
    {
        
        [Required(ErrorMessage = "The meeting needs to have a description")]
        public string Description { get; set; }
        [Required(ErrorMessage = "All the time options need to be filled.")]
        public DateTime Option1 { get; set; }
        [Required(ErrorMessage = "All the time options need to be filled.")]
        public DateTime Option2 { get; set; }
        [Required(ErrorMessage = "All the time options need to be filled.")]
        public DateTime Option3 { get; set; }
        [Required(ErrorMessage = "The meeting needs to have a time duration")]
        public TimeSpan Duration { get; set; }
        public ApplicationUser Author { get; set; }
        public List<ApplicationUser> AllUsers { get; set; }
        public List<ApplicationUser> InvitedUsers { get; set;
        }

    }
}