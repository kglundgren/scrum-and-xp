using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using scrum_and_xp.Models;

namespace scrum_and_xp.App_Start
{
    public class SchedulerInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        

    }
}
