namespace FinalProject_LMS.Migrations
{
    using FinalProject_LMS.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<FinalProject_LMS.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(FinalProject_LMS.Models.ApplicationDbContext context)
        {
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new ApplicationUserManager(userStore);

            var emails = new[] { "teacher1@lexicon.se", "teacher2@lexicon.se", "student1@lexicon.se" };

            foreach (var email in emails)
            {
                int position = email.IndexOf("@");
                if (position < 0)
                    continue;
                string userName = email.Substring(0, position);

                if (context.Users.Any(u => u.UserName == email)) continue;

                var user = new ApplicationUser { UserName = email, Email = email, Name = userName };
                var result = userManager.Create(user, "P@ssw0rd");
                if (!result.Succeeded)
                {
                    throw new Exception(string.Join("\n", result.Errors));
                }
            }


            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);

            var roleNames = new[] { "Teacher", "Student" };
            foreach (var roleName in roleNames)
            {
                if (context.Roles.Any(r => r.Name == roleName)) continue;

                var role = new IdentityRole { Name = roleName };
                var result = roleManager.Create(role);
                if (!result.Succeeded)
                {
                    throw new Exception(string.Join("\n", result.Errors));
                }
            }

            var adminUser = userManager.FindByName("teacher1@lexicon.se");
            userManager.AddToRole(adminUser.Id, "Teacher");

            var editorUser = userManager.FindByName("teacher2@lexicon.se");
            userManager.AddToRole(editorUser.Id, "Teacher");

            var john = userManager.FindByName("student1@lexicon.se");
            userManager.AddToRoles(john.Id, "Student");

        }
    }
}
