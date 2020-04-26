using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using scrum_and_xp.Models;

namespace scrum_and_xp.Controllers
{
    public class FileUploadController : Controller
    {
        // GET: FileUpload

        public string ErrorMessage { get; set; }
        
        public string ValidateUpload(HttpPostedFileBase file)
        {
            try
            {
                var supportedTypes = new[] { "txt", "doc", "docx", "pdf","txt", "jpg", "png" };
                var fileExt = System.IO.Path.GetExtension(file.FileName).Substring(1);
                if (!supportedTypes.Contains(fileExt))
                {
                    ErrorMessage = "File Extension Is InValid - Only Upload WORD/PDF/TXT/JPG/PNG File";
                    return ErrorMessage;
                }
                else
                {
                    ErrorMessage = "File Is Successfully Uploaded";
                    return ErrorMessage;
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = "Upload Container Should Not Be Empty or Contact Admin";
                return ErrorMessage;
            }
        }
    }
}