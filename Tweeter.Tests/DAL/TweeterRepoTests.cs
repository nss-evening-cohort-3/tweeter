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

        public Mock<DbSet<Twit>> mock_users { get; set; }
        public Mock<DbSet<Tweet>> mock_tweets { get; set; }
        public Mock<TweeterContext> mock_context { get; set; }
        public TweeterRepository Repo { get; set; }
        public List<Twit> users { get; set; }
        public List<Tweet> tweets { get; set; }

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
            tweets = new List<Tweet>
            {
                new Tweet
                {
                    TweetId = 1,
                    Message = "Hey dude."
                },
                new Tweet
                {
                    TweetId = 2,
                    Message = "Hey buddy."
                }
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
            mock_users.Setup(u => u.Remove(It.IsAny<Twit>())).Callback((Twit t) => users.Remove(t));

            var query_tweets = tweets.AsQueryable();
            mock_tweets.As<IQueryable<Tweet>>().Setup(m => m.Provider).Returns(query_tweets.Provider);
            mock_tweets.As<IQueryable<Tweet>>().Setup(m => m.Expression).Returns(query_tweets.Expression);
            mock_tweets.As<IQueryable<Tweet>>().Setup(m => m.ElementType).Returns(query_tweets.ElementType);
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
        public void CanGetAllTweets()
        {
            //Arrange
            ConnectToDatastore();
         
            //Act    
            List<Tweet> tweets = Repo.GetTweets();
            int expected = 2;
            int actual = tweets.Count();
            
            //Assert 
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CanAddTweet()
        {
            //Arrange 
            ConnectToDatastore();

            //Act
            Tweet bogus_tweet = new Tweet { TweetId = 3, Message = "Hey homes." };
            Repo.AddTweet(bogus_tweet);
            List<Tweet> tweets = Repo.GetTweets();

            //Assert
            Assert.AreEqual(3, tweets.Count());
        }

        [TestMethod]
        public void CanDeleteTweet()
        {
            //Arrange
            ConnectToDatastore();

            //Act
            Tweet bogus_tweet = new Tweet { TweetId = 3, Message = "Hey homes." };
            Repo.AddTweet(bogus_tweet);
            Repo.DeleteTweet(bogus_tweet);
            List<Tweet> tweets = Repo.GetTweets();

            //Assert
            Assert.AreEqual(2, tweets.Count());
        }
    }
}