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
    public class Tweet_TweeterRepoTests
    {
        private Mock<DbSet<Tweet>> mock_tweets { get; set; }
        private Mock<TweeterContext> mock_context { get; set; }
        private TweeterRepository Repo { get; set; }
        private List<Tweet> tweets { get; set; }

        [TestInitialize]
        public void Initialize()
        {
            mock_context = new Mock<TweeterContext>();
            mock_tweets = new Mock<DbSet<Tweet>>();
            Repo = new TweeterRepository(mock_context.Object);

            tweets = new List<Tweet>
            {
                new Tweet {
                    TweetId = 1,
                    Message = "This is the first tweet"
                },
                new Tweet {
                    TweetId = 2,
                    Message = "This is the second tweet"
                }
            };
        }

        public void ConnectToDatastore()
        {
            var query_tweets = tweets.AsQueryable();

            mock_tweets.As<IQueryable<Tweet>>().Setup(m => m.Provider).Returns(query_tweets.Provider);
            mock_tweets.As<IQueryable<Tweet>>().Setup(m => m.Expression).Returns(query_tweets.Expression);
            mock_tweets.As<IQueryable<Tweet>>().Setup(m => m.ElementType).Returns(query_tweets.ElementType);
            mock_tweets.As<IQueryable<Tweet>>().Setup(m => m.GetEnumerator()).Returns(() => query_tweets.GetEnumerator());

            mock_context.Setup(c => c.Tweets).Returns(mock_tweets.Object);
            mock_tweets.Setup(u => u.Add(It.IsAny<Tweet>())).Callback((Tweet t) => tweets.Add(t));
            /*
             * Below mocks the 'Users' getter that returns a list of ApplicationUsers
             * mock_user_manager_context.Setup(c => c.Users).Returns(mock_users.Object);
             * 
             */

            /* IF we just add a Username field to the Twit model
             * mock_context.Setup(c => c.TweeterUsers).Returns(mock_users.Object); Assuming mock_users is List<Twit>
             */
        }

        [TestMethod]
        public void RepoEnsureCanCreateInstance()
        {
            TweeterRepository repo = new TweeterRepository();
            Assert.IsNotNull(repo);
        }

        [TestMethod]
        public void Tweet_CanIAddATweet()
        {
            // Arrange
            ConnectToDatastore();
            Tweet testTweet = new Tweet
            {
                TweetId = 3,
                Message = "This is message three"
            };

            // Act
            int returnedTweetCount = Repo.AddTweet(testTweet);

            // Assert
            Assert.AreEqual(3, returnedTweetCount);
        }
        [TestMethod]
        public void Tweet_CanIRemoveATweet()
        {
            // Arrange
            ConnectToDatastore();

            // Act
            int removedTweetCount = Repo.RemoveTweet(2);

            // Assert
            Assert.AreEqual(3, removedTweetCount);
        }
    }
}
