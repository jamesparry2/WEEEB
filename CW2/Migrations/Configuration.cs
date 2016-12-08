namespace CW2.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using CW2.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<CW2.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CW2.Models.ApplicationDbContext context)
        {
            createRoles(context);
            AddStudnet(context);
            AddLecturer(context);

        }

        //A method to create the roles for the application
        void createRoles(CW2.Models.ApplicationDbContext context)
        {
            context.Roles.AddOrUpdate(r => r.Name,
                new IdentityRole { Name = "Lecturer" },
                new IdentityRole { Name = "Student" }
                );
        }

        //Create a method that seeds the Database with a Lecture account
        // and assigns their roles to them
        void AddLecturer(CW2.Models.ApplicationDbContext context)
        {
            var Lecturer1 = new ApplicationUser { UserName = "Lecturer1@email.com" };
            var um1 = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(context));
            um1.Create(Lecturer1, "password");

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var id1 = userManager.FindByName("Lecturer1@email.com");
            um1.AddToRole(id1.Id, "Lecturer");

        }

        //A method that seeds that database with Students accounts and assigns
        //their roles to them.        
        void AddStudnet(CW2.Models.ApplicationDbContext context)
        {
            var student1 = new ApplicationUser { UserName = "Student1@email.com" };
            var student2 = new ApplicationUser { UserName = "Student2@email.com" };
            var student3 = new ApplicationUser { UserName = "Student3@email.com" };
            var student4 = new ApplicationUser { UserName = "Student4@email.com" };
            var student5 = new ApplicationUser { UserName = "Student5@email.com" };

            var um1 = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(context));
            var um2 = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(context));
            var um3 = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(context));
            var um4 = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(context));
            var um5 = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(context));

            um1.Create(student1, "password");
            um2.Create(student2, "password");
            um3.Create(student3, "password");
            um4.Create(student4, "password");
            um5.Create(student5, "password");

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var id1 = userManager.FindByName("Student1@email.com");
            var id2 = userManager.FindByName("Student2@email.com");
            var id3 = userManager.FindByName("Student3@email.com");
            var id4 = userManager.FindByName("Student4@email.com");
            var id5 = userManager.FindByName("Student5@email.com");
            um1.AddToRole(id1.Id, "Student");
            um2.AddToRole(id2.Id, "Student");
            um3.AddToRole(id3.Id, "Student");
            um4.AddToRole(id4.Id, "Student");
            um5.AddToRole(id5.Id, "Student");
        }
    }
}
