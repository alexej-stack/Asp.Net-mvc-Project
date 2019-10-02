using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Task2.Models
{
    public enum CompanyTematic
    {
        Electronic,
        Nature,
        Education,
        kitchen,
        Cars,
        Realty
    }
    public class Company
    {
        [Key]
        public int ID { get; set; }
        public string tName { get; set; }
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        public string UserImage { get; set; }
        public string Video { get; set; }
        public string Galery { get; set; }
        public CompanyTematic Tematic { get; set; }
        // public DateTime DOB { get; set; }
        public string aspNetUser { get; set; }
        public string Votes { get; set; }
        public double Money { get; set; }
        //public AspNetUser aspNetUser { get; set; }
        public ICollection<Bonus> Bonus{ get; set; }
        public Company()
        {
            Bonus = new List<Bonus>();
        }
    }

}