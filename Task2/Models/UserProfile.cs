using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Task2.Models
{
    public class UserProfile
    {
        public string ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DOB { get; set; }
       // public AspNetUser aspNetUser { get; set; }
    }
}