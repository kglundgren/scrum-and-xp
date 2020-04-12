using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace scrum_and_xp.Models
{
    public abstract class SuperCat
    {
        public int Id { get; set; }
        public string Name { get; set; }
       
    }

    public class FormalCategory : SuperCat
    {
        public virtual ICollection<FormalCustom> ParentCat { get; set; }
    }
    public  class InformalCategory : SuperCat
    {
        public virtual ICollection<InformalCustom> ParentCat { get; set; }
    }
    //public class FormalWork : FormalCategory
    //{

    //}

    //public class FormalResearch : FormalCategory
    //{

    //}
    //public class FormalEducation : FormalCategory
    //{

    //}

    //    public abstract class Custom
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
        
      
    //}

    public class FormalCustom : SuperCat
    {
        
    }
    public class InformalCustom : SuperCat
    {
        
        
    }






}