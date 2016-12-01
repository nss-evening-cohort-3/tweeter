namespace Tweeter.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Tweeter.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<Tweeter.DAL.TweeterContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Tweeter.DAL.TweeterContext context)
        {
            
            ApplicationUser fake_user = new ApplicationUser
            {
                Id = "fake-user-id",
                EmailConfirmed = false,
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnabled = false,
                AccessFailedCount = 0,
                UserName = "TweeterBot"

            };

            Twit twit_bot = new Twit { TwitId = 1, BaseUser = fake_user };

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
                new Tweet { Message = "Welcome to Tweeter!", CreatedAt = DateTime.Now, TweetId = 1 },
                new Tweet { Message = "I'm here to help you", CreatedAt = DateTime.Now, TweetId = 2 },
                new Tweet { Message = "Follow me for usage tips", ImageURL = "", CreatedAt = DateTime.Now, Replies = new List<Tweet> { }, TweetId = 3 }
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
