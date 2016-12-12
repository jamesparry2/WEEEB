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
            CreateRoles(context);
            AddStudnet(context);
            AddLecturer(context);
        }

        //A method to create the roles for the application
        void CreateRoles(CW2.Models.ApplicationDbContext context)
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
            var Um1 = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(context));
            Um1.Create(Lecturer1, "password");

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var id1 = userManager.FindByName("Lecturer1@email.com");
            Um1.AddToRole(id1.Id, "Lecturer");

        }

        //A method that seeds that database with Students accounts and assigns
        //their roles to them.        
        void AddStudnet(CW2.Models.ApplicationDbContext context)
        {
            var Student1 = new ApplicationUser { UserName = "Student1@email.com" };
            var Student2 = new ApplicationUser { UserName = "Student2@email.com" };
            var Student3 = new ApplicationUser { UserName = "Student3@email.com" };
            var Student4 = new ApplicationUser { UserName = "Student4@email.com" };
            var Student5 = new ApplicationUser { UserName = "Student5@email.com" };

            var Um1 = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(context));
            var Um2 = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(context));
            var Um3 = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(context));
            var Um4 = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(context));
            var Um5 = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(context));

            Um1.Create(Student1, "password");
            Um2.Create(Student2, "password");
            Um3.Create(Student3, "password");
            Um4.Create(Student4, "password");
            Um5.Create(Student5, "password");

            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var Id1 = UserManager.FindByName("Student1@email.com");
            var Id2 = UserManager.FindByName("Student2@email.com");
            var Id3 = UserManager.FindByName("Student3@email.com");
            var Id4 = UserManager.FindByName("Student4@email.com");
            var Id5 = UserManager.FindByName("Student5@email.com");
            Um1.AddToRole(Id1.Id, "Student");
            Um2.AddToRole(Id2.Id, "Student");
            Um3.AddToRole(Id3.Id, "Student");
            Um4.AddToRole(Id4.Id, "Student");
            Um5.AddToRole(Id5.Id, "Student");
        }
    }
}
