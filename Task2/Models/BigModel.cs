using Korzh.EasyQuery.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Task2.Models
{
    public class BigModel
{
    public Company Company { get; set; }
        public IEnumerable<Company> Companies { get; set; }
        public Bonus Bonus { get; set; }
        public CompanyViewModel CompanyViewModel { get; set; }
        public AlbumMaster Album { get; set; }
        public UserProfile UserProfile { get; set; }
       
        // public IEnumerable<UserProfile> UserProfile { get; set; }
    }
}