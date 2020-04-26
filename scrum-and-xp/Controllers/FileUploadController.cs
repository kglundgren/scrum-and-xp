using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using scrum_and_xp.Models;

namespace scrum_and_xp.Controllers
{
    public class FileUpload
    {
        // GET: FileUpload


        public bool ValidateUpload(HttpPostedFileBase file)
        {

            var supportedTypes = new[] { "txt", "doc", "docx", "pdf", "txt", "jpg", "png" };
            var fileExt = System.IO.Path.GetExtension(file.FileName).Substring(1);
            if (!supportedTypes.Contains(fileExt))
            {
                return false;
            }
            else
            {
                return true;
            }

        }
    }
}