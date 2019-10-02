using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Task2.Models
{
    public class Bonus
    {[Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<AspNetUser> AspNetUsers { get; set; }
        public Company Company { get; set; }
        public double price { get; set; }
        public Bonus()
        {
            AspNetUsers = new List<AspNetUser>();
           
        }
    }
}