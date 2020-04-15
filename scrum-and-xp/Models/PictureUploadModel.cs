using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace scrum_and_xp.Models
{
    public class PictureUploadModel : Controller
    {
        [DataType(DataType.Upload)]
        [Display(Name = "Upload File")]
        [Required(ErrorMessage = "Välj en bild att ladda upp.")]
        public string picture { get; set; }
    }
}