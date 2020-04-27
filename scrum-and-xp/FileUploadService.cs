using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace scrum_and_xp
{
    public class FileUploadService { 
     public void Upload(HttpPostedFileBase file)
    {
        
        
            if (file.ContentLength>0)
            {
                string filename = Path.GetFileName(file.FileName);
               //string filepath = Path.Combine(Server.MapPath("~/UploadedFiles"), filename);
                file.SaveAs("~/UploadedFiles"+ filename);
            }
    

    }
    }
}