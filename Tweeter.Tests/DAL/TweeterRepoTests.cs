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
    public class TweeterRepoTests
    {

        private Mock<DbSet<Twit>> mock_users { get; set; }

        private Mock<DbSet<Tweet>> mock_tweets { get; set; }
        private Mock<TweeterContext> mock_context { get; set; }
        private TweeterRepository Repo { get; set; }
        private List<Twit> users { get; set; }

        private List<Tweet> tweets { get; set; }

        [TestInitialize]
        public void Initialize()
        {
            mock_context = new Mock<TweeterContext>();
            mock_users = new Mock<DbSet<Twit>>();
            mock_tweets = new Mock<DbSet<Tweet>>();
            Repo = new TweeterRepository(mock_context.Object);
            users = new List<Twit>
            {
                new Twit {
                    TwitId = 1,
                    BaseUser = new ApplicationUser() { UserName = "michealb"}
                },
                new Twit {
                    TwitId = 2,
                    BaseUser = new ApplicationUser() { UserName = "sallym"}
                }

            };

            tweets = new List<Tweet>()
            {
                new Tweet { Message = "WHAT's Good!!!", TweetId = 1},

                new Tweet { Message = "WHAT's REALLY Good!!!", TweetId = 2 }
            };

            /* 
             1. Install Identity into Tweeter.Tests (using statement needed)
             2. Create a mock context that uses 'UserManager' instead of 'TweeterContext'
             */
        }

        public void ConnectToDatastore()
        {
            var query_users = users.AsQueryable();

            mock_users.As<IQueryable<Twit>>().Setup(m => m.Provider).Returns(query_users.Provider);
            mock_users.As<IQueryable<Twit>>().Setup(m => m.Expression).Returns(query_users.Expression);
            mock_users.As<IQueryable<Twit>>().Setup(m => m.ElementType).Returns(query_users.ElementType);
            mock_users.As<IQueryable<Twit>>().Setup(m => m.GetEnumerator()).Returns(() => query_users.GetEnumerator());

            mock_context.Setup(c => c.TweeterUsers).Returns(mock_users.Object);
            mock_users.Setup(u => u.Add(It.IsAny<Twit>())).Callback((Twit t) => users.Add(t));

            /* FOR THE TWEETS */

            var query_tweets = tweets.AsQueryable();

            mock_tweets.As<IQueryable<Tweet>>().Setup(m => m.Provider).Returns(query_users.Provider);
            mock_tweets.As<IQueryable<Tweet>>().Setup(m => m.Expression).Returns(query_users.Expression);
            mock_tweets.As<IQueryable<Tweet>>().Setup(m => m.ElementType).Returns(query_users.ElementType);
            mock_tweets.As<IQueryable<Tweet>>().Setup(m => m.GetEnumerator()).Returns(() => query_tweets.GetEnumerator());

            mock_context.Setup(c => c.Tweets).Returns(mock_tweets.Object);
            mock_tweets.Setup(u => u.Add(It.IsAny<Tweet>())).Callback((Tweet t) => tweets.Add(t));

            mock_tweets.Setup(u => u.Remove(It.IsAny<Tweet>())).Callback((Tweet t) => tweets.Remove(t));
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
        public void RepoEnsureICanGetUsernames()
        {
            // Arrange
            ConnectToDatastore();

            // Act
            List<string> usernames = Repo.GetUsernames();

            // Assert
            Assert.AreEqual(2, usernames.Count);
        }

        [TestMethod]
        public void RepoEnsureUsernameExists()
        {
            // Arrange
            ConnectToDatastore();

            // Act
            bool exists = Repo.UsernameExists("sallym");

            // Assert
            Assert.IsTrue(exists);
        }

        [TestMethod]
        public void RepoEnsureUsernameExistsOfTwit()
        {
            // Arrange
            ConnectToDatastore();

            // Act
            Twit found_twit = Repo.UsernameExistsOfTwit("sallym");

            // Assert
            Assert.IsNotNull(found_twit);
        }

        [TestMethod]
        public void CanIAddATweet()
        {
            // Arrange
            TweeterRepository repo = new TweeterRepository(mock_context.Object);
            ConnectToDatastore();
            Tweet newTweet = new Tweet() { Message = "Yaaahhh Mon" };


            // Act
            repo.AddTweet(newTweet);

            // Assert
            Assert.IsTrue(tweets.Contains(newTweet));
        }

        [TestMethod]
        public void CanIRemoveATweet()
        {
            // Arrange
            TweeterRepository repo = new TweeterRepository(mock_context.Object);
            ConnectToDatastore();
            Tweet newTweet = new Tweet() { Message = "Yaaahhh Mon" };
            repo.AddTweet(newTweet);

            // Act
            repo.RemoveTweet(newTweet);

            // Assert
            Assert.IsFalse(tweets.Contains(newTweet));
        }
    }
}
