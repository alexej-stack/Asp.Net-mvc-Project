using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using Task2;
using Task2.Models;

namespace RolesIdentityApp.Models
{
    public class AppDbInitializer : DropCreateDatabaseAlways<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

 
            var role1 = new IdentityRole { Name = "admin" };
            var role2 = new IdentityRole { Name = "user" };
            var role3 = new IdentityRole { Name = "viewer"};

            roleManager.Create(role1);
            roleManager.Create(role2);
            roleManager.Create(role3);

            var admin = new ApplicationUser { Email = "somemail@mail.ru", UserName = "somemail@mail.ru",EmailConfirmed=true,IsEnable="true" };
            string password = "ad46D_ewr3";
            
            var result = userManager.Create(admin, password);

            
            if (result.Succeeded)
            { 
                userManager.AddToRole(admin.Id, role1.Name);
                userManager.AddToRole(admin.Id, role2.Name);
            }

            base.Seed(context);
        }
    }
}