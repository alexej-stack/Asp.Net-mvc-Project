using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Task2.Models
{
    
        public class AspNetUser
        {
         
            public string Id { get; set; }
         
            public string Email { get; set; }
           
           public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
      
        public bool LockoutEnabled { get; set; }
        public string UserName { get; set; }
        public string IsEnable { get; set; }
    //    public Guid ActivationCode { get; set; }
       // public UserProfile userProfile { get; set; }
        public ICollection<Bonus> Bonus { get; set; }
        public ICollection<Company> companies { get; set; }
        public AspNetUser() {
            Bonus = new List<Bonus>();
            companies = new List<Company>();
        }
    }
}