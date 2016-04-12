namespace TechHelperPOC.Web.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using TechHelperPOC.Web.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<TechHelperPOC.Web.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(TechHelperPOC.Web.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            var passwordHash = new PasswordHasher();
            string password = passwordHash.HashPassword("Password@123");
            //string id = passwordHash.HashPassword("admin");
            context.Users.AddOrUpdate(u=>u.UserName,
                new ApplicationUser
                {
                    //Id = id,
                    Email = "admin@winaudit.biz",
                    SecurityStamp=password,
                    UserName = "admin@winaudit.biz",
                    PasswordHash=password
                }
                );

            //var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            //string[] roleNames = {"Admin", "Member" };
            //IdentityResult roleResult;
            //foreach (var roleName in roleNames)
            //{
            //    if (!RoleManager.RoleExists(roleName))
            //    {
            //        roleResult = RoleManager.Create(new IdentityRole(roleName));
            //    }
            //}
            //var id1 = passwordHash.HashPassword("Admin");
            //var id2 = passwordHash.HashPassword("Member");
            context.Roles.AddOrUpdate(
                r=>r.Name,
                new IdentityRole {Name="Admin"},
                new IdentityRole {Name="Member"}
                );

            //var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            //UserManager.AddToRole("AN+124bUV+Has/KCLgCcQEoYm6NI+f6x7GRN+Es29GiPDBvc8U34N32hi/Ebg/jhcQ==", "Admin");
        }
    }
}
