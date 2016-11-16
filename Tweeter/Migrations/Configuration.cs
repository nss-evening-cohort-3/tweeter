namespace Tweeter.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Microsoft.AspNet.Identity.EntityFramework;

    internal sealed class Configuration : DbMigrationsConfiguration<Tweeter.DAL.TweeterContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Tweeter.DAL.TweeterContext context)
        {
            context.TweeterUsers.AddOrUpdate(
                t => t.TwitName,
                new Models.Twit { TwitName = "Bob", TwitId = 1, BaseUser = new Models.ApplicationUser { Id = "1", Email = "bob@bob.com", UserName = "Bob" } },
                new Models.Twit { TwitName = "George", TwitId = 2, BaseUser = new Models.ApplicationUser { Id = "2", Email = "george@bob.com", UserName = "George" } }
                );
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
        }
    }
}
