using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Task2.Models
{
    public class CompanyViewModel
    {
        public IQueryable<Company> Company { get; set; }
        public string text {get;set;}
    }
    public class AllCoViewModel
    {
        
       
        public int CoId { get; set; }
      
        public string Description { get; set; }
        public string Title { get; set; }
     
        public bool Checked { get; set; }
   
   
    }
}