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
        private Mock<DbSet<Tweet>> mock_tweets { get; set; }
        private Mock<DbSet<Twit>> mock_users { get; set; }
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

            
            tweets = new List<Tweet>();
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
            Repo = new TweeterRepository(mock_context.Object);
            ConnectToDatastore();
            /* 
             1. Install Identity into Tweeter.Tests (using statement needed)
             2. Create a mock context that uses 'UserManager' instead of 'TweeterContext'
             */
        }

        public void ConnectToDatastore()
        {
            var query_users = users.AsQueryable();
            var query_tweets = tweets.AsQueryable();

            mock_users.As<IQueryable<Twit>>().Setup(m => m.Provider).Returns(query_users.Provider);
            mock_users.As<IQueryable<Twit>>().Setup(m => m.Expression).Returns(query_users.Expression);
            mock_users.As<IQueryable<Twit>>().Setup(m => m.ElementType).Returns(query_users.ElementType);
            mock_users.As<IQueryable<Twit>>().Setup(m => m.GetEnumerator()).Returns(() => query_users.GetEnumerator());

            mock_tweets.As<IQueryable<Tweet>>().Setup(t => t.Provider).Returns(query_tweets.Provider);
            mock_tweets.As<IQueryable<Tweet>>().Setup(t => t.Expression).Returns(query_tweets.Expression);
            mock_tweets.As<IQueryable<Tweet>>().Setup(t => t.ElementType).Returns(query_tweets.ElementType);
            mock_tweets.As<IQueryable<Tweet>>().Setup(t => t.GetEnumerator()).Returns(() => query_tweets.GetEnumerator());

            mock_context.Setup(c => c.TweeterUsers).Returns(mock_users.Object);
            mock_users.Setup(u => u.Add(It.IsAny<Twit>())).Callback((Twit t) => users.Add(t));

            
            mock_tweets.Setup(d => d.Add(It.IsAny<Tweet>())).Callback((Tweet d) => tweets.Add(d));
            mock_tweets.Setup(v => v.Remove(It.IsAny<Tweet>())).Callback((Tweet v) => tweets.Remove(v));


            mock_context.Setup(c => c.Tweets).Returns(mock_tweets.Object);
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
        public void EnsureCanGetAllTweets()
        {
            Tweet tweet1 = new Tweet { TweetId = 1, Message = "Hi" };
            Tweet tweet2 = new Tweet { TweetId = 2, Message = "Yo" };
            Tweet tweet3 = new Tweet { TweetId = 3, Message = "Boo"};
            Repo.Add(tweet1);
            Repo.Add(tweet2);
            Repo.Add(tweet3);

            int expected_count = 3;
            List<Tweet> all_tweets = Repo.GetAllTweets();
            int actual_count = all_tweets.Count;

            Assert.AreEqual(expected_count, actual_count);
        }

        [TestMethod]
        public void EnsureCanAddNewTweet()
        {
            

            Tweet tweet1 = new Tweet {TweetId = 1, Message = "Hi" };
            Tweet tweet2 = new Tweet { TweetId = 2, Message = "Yo" };
            Repo.Add(tweet1);
            Repo.Add(tweet2);

            int expected_count = 2;
            List<Tweet> all_tweets = Repo.GetAllTweets();
            int actual_count = all_tweets.Count;

            Assert.AreEqual(expected_count, actual_count);
        }

        [TestMethod]
        public void EnsureCanDeleteATweet()
        {
            

            Tweet tweet1 = new Tweet { TweetId = 1, Message = "Hi" };
            Tweet tweet2 = new Tweet { TweetId = 2, Message = "Yo" };
            Repo.Add(tweet1);
            Repo.Add(tweet2);

            Repo.DeleteTweet(tweet1);

            int expected_count = 1;
            List<Tweet> all_tweets= Repo.GetAllTweets();
            int actual_count = all_tweets.Count;

            Assert.AreEqual(expected_count, actual_count);
        }
    }
}
