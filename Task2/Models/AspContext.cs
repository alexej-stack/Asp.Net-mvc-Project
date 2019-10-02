using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNet.Identity;

namespace Task2.Models
{
 
    public class AspContext : DbContext
    {
        public DbSet<VoteLog> VoteLogs { get; set; }
        public DbSet<AspNetUser> AspNetUsers { get; set; }
      
        public DbSet<Bonus> Bonus { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<CompanyComment> CompanyComments { get; set; }
        public DbSet<Company> Companies { get; set; }
       // public DbSet<AlbumMaster> AlbumMasters { get; set; }

        /*public AspContext(DbContextOptions<AspContext> options)
    : base(options)
        {
            Database.EnsureCreated();
        }*/
        /*protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<AspNetUser>()
    .HasMany(p => p.companies)
    .WithRequired(p => p.aspNetUser);
        }*/

    }
}