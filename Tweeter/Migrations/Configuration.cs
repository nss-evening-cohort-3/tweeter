namespace Tweeter.Migrations
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Tweeter.DAL.TweeterContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Tweeter.DAL.TweeterContext context)
        {
            // May not need this fake twit_bot at all...
            //Twit twit_bot = new Twit { BaseUser = new ApplicationUser { UserName = "TwitBot" } };
            Twit twit_bot = new Twit { TwitId = 2 };

            context.TweeterUsers.AddOrUpdate(
                p => p.TwitId,
                twit_bot
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
