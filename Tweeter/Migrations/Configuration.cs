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
                new Models.Twit { TwitId = 1, TwitName = "Bob" },
                new Models.Twit { TwitId = 2, TwitName = "Joe" });
            context.Tweets.AddOrUpdate(
                tweet => tweet.TweetId,
                new Models.Tweet { TweetId = 1, TwitName = new Models.Twit { TwitId = 1, TwitName = "Bob" }, Message = "Hi, I'm Bob!" },
                new Models.Tweet { TweetId = 2, TwitName = new Models.Twit { TwitId = 2, TwitName = "Joe" }, Message = "Go to hell, Bob." });
        }
    }
}