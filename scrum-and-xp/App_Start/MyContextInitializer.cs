using scrum_and_xp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace scrum_and_xp.App_Start
{
    public class MyContextInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
    }
}