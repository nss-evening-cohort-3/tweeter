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

            ApplicationUser fake_user = new ApplicationUser
            {
                Id = "fake-user-id",
                EmailConfirmed = false,
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnabled = false,
                AccessFailedCount = 0,
                UserName = "TweeterBot",
            };

            Twit twit_bot = new Twit { TwitId = 2, BaseUser = fake_user };

            context.Users.AddOrUpdate(
                u => u.Id,
                fake_user
                );

            context.TweeterUsers.AddOrUpdate(
                p => p.TwitId,
                twit_bot
                );

            context.Tweets.AddOrUpdate(
                t => t.TweetId,
                new Tweet { Message = "This is a third Tweet",  ImageURL= "http://placehold.it/350x175", CreatedAt = DateTime.Now, TweetId = 9 },
                new Tweet { Message = "This is a fourth Tweet", ImageURL = "http://placehold.it/350x175", CreatedAt = DateTime.Now, TweetId = 10 },
                new Tweet { Message = "This is a fifth Tweet", ImageURL = "http://placehold.it/350x175", CreatedAt = DateTime.Now, TweetId = 11 }
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
