using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tweeter.DAL;
using Moq;
using System.Data.Entity;
using Tweeter.Models;
using System.Collections.Generic;
using System.Linq;


namespace Tweeter.Tests.DAL
{
    [TestClass]
    public class TweetRepoTests
    {

        private Mock<TweeterContext> mock_context { get; set; }
        private Mock<DbSet<Tweet>> mock_tweets { get; set; }
        private List<Tweet> tweets { get; set; }
        private TweeterRepository repo { get; set; }
        private Twit Bob;
        private Twit Joe;
        private Tweet new_tweet;
        private Tweet last_tweet;
        public void ConnectToDatastore()
        {
            var query_tweets = tweets.AsQueryable();

            mock_tweets.As<IQueryable<Tweet>>().Setup(m => m.Provider).Returns(query_tweets.Provider);
            mock_tweets.As<IQueryable<Tweet>>().Setup(m => m.Expression).Returns(query_tweets.Expression);
            mock_tweets.As<IQueryable<Tweet>>().Setup(m => m.ElementType).Returns(query_tweets.ElementType);
            mock_tweets.As<IQueryable<Tweet>>().Setup(m => m.GetEnumerator()).Returns(() => query_tweets.GetEnumerator());

            mock_context.Setup(m => m.Tweets).Returns(mock_tweets.Object);

            mock_tweets.Setup(t => t.Add(It.IsAny<Tweet>())).Callback((Tweet t) => tweets.Add(t));
            mock_tweets.Setup(t => t.Remove(It.IsAny<Tweet>())).Callback((Tweet t) => tweets.Remove(t));


            /*
             * Below mocks the 'Users' getter that returns a list of ApplicationUsers
             * mock_user_manager_context.Setup(c => c.Users).Returns(mock_users.Object);
             * 
             */

            /* IF we just add a Username field to the Twit model
             * mock_context.Setup(c => c.TweeterUsers).Returns(mock_users.Object); Assuming mock_users is List<Twit>
             */
        }

        [TestInitialize]
        public void Initialize()
        {
            mock_context = new Mock<TweeterContext>();
            mock_tweets = new Mock<DbSet<Tweet>>();
            repo = new TweeterRepository(mock_context.Object);
            tweets = new List<Tweet>();
            Bob = new Twit { TwitName = "Bob", TwitId = 0 };
            Joe = new Twit { TwitName = "Joe", TwitId = 1 };
            new_tweet = new Tweet { TweetId = 1, Message = "Hi, I'm Bob!" };
            last_tweet = new Tweet { TweetId = 2, Message = "Go to hell, Bob." };
            tweets.Add(new_tweet);tweets.Add(last_tweet);
            ConnectToDatastore();

            /* 
             1. Install Identity into Tweeter.Tests (using statement needed)
             2. Create a mock context that uses 'UserManager' instead of 'TweeterContext'
             */
        }
        [TestCleanup]
        public void TearDown()
        {
            repo = null;
        }
        [TestMethod]
        public void EnsureContext()
        {
            Assert.IsNotNull(repo);
        }
        [TestMethod]
        public void EnsureCanGetTweets()
        {
            var tweets = repo.GetTweets();
            Assert.IsInstanceOfType(tweets, typeof(List<Tweet>));
        }
        [TestMethod]
        public void EnsureCanGetTweetByTweetId()
        {
            var tweet = repo.GetTweets(1);
            Console.WriteLine(tweet[0]);
            Assert.IsTrue(tweet[0].TweetId == 1);
        }

        [TestMethod]
        public void EnsureCanAddTweet()
        {
            Tweet third_tweet = new Tweet { TweetId = 3, Message = "Rude! I prefer civil discourse in 140 characters or less."};
            repo.AddTweet(third_tweet);
            int expectedtweets = 3;
            int actualtweets = repo.GetTweets().Count();
            Assert.AreEqual(expectedtweets, actualtweets);
        }
        [TestMethod]
        public void EnsureCanRemoveTweet()
        {
            repo.RemoveTweet(last_tweet);
            int expectedtweets = 1;
            int actualtweets = repo.GetTweets().Count();
            Assert.AreEqual(expectedtweets, actualtweets);
        }
        [TestMethod]
        public void EnsureCanRemoveTweetByTweetId()
        {
            Assert.IsTrue(repo.GetTweets().Count() == 2);
            repo.RemoveTweet(2);
            Assert.IsTrue(repo.GetTweets().Count() == 1);
        }
    }
}