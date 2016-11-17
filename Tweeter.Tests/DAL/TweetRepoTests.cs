﻿using System;
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
        private Mock<DbSet<Twit>> mock_tweets { get; set; }
        private List<Twit> tweets { get; set; }
        private TweeterRepository repo { get; set; }
        private Twit Bob;
        private Twit Joe;
        public void ConnectToDatastore()
        {
            var query_tweets = tweets.AsQueryable();

            mock_tweets.As<IQueryable<Twit>>().Setup(m => m.Provider).Returns(query_tweets.Provider);
            mock_tweets.As<IQueryable<Twit>>().Setup(m => m.Expression).Returns(query_tweets.Expression);
            mock_tweets.As<IQueryable<Twit>>().Setup(m => m.ElementType).Returns(query_tweets.ElementType);
            mock_tweets.As<IQueryable<Twit>>().Setup(m => m.GetEnumerator()).Returns(() => query_tweets.GetEnumerator());

            mock_context.Setup(m => m.Tweets).Returns(mock_tweets.Object);

            mock_tweets.Setup(t => t.Add(It.IsAny<Twit>())).Callback((Twit t) => tweets.Add(t));
            mock_tweets.Setup(t => t.Remove(It.IsAny<Twit>())).Callback((Twit t) => tweets.Remove(t));


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
            mock_tweets = new Mock<DbSet<Twit>>();
            repo = new TweeterRepository(mock_context.Object);
            tweets = new List<Twit>();
            Bob = new Twit { TwitName = "Bob", TwitId = 0 };
            Joe = new Twit { TwitName = "Joe", TwitId = 1 };
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
        public void EnsureCanAddTweet()
        {
            Tweet new_tweet = new Tweet { TwitName = Bob, TweetId = 1, Message = "Hi, I'm Bob!" };
            repo.AddTweet(new_tweet);
            int expectedtweets = 1;
            int actualtweets = repo.GetTweets().Count();
            Assert.AreEqual(expectedtweets, actualtweets);
        }
        [TestMethod]
        public void EnsureCanRemoveTweet()
        {
            Tweet new_tweet = new Tweet { TweetId = 1, Message = "Hi, I'm Bob!" };
            Tweet last_tweet = new Tweet { TweetId = 2, Message = "Go to hell, Bob." };
            repo.AddTweet(new_tweet); repo.AddTweet(last_tweet);
            repo.RemoveTweet(last_tweet);
            int expectedtweets = 1;
            int actualtweets = repo.GetTweets().Count();
            Assert.AreEqual(expectedtweets, actualtweets);
        }
    }
}