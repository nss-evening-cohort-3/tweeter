using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tweeter.DAL;
using Moq;
using Tweeter.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Tweeter.Tests
{
    [TestClass]
    public class TweeterRepoTests
    {
        Mock<TweeterContext> mock_context { get; set; }
        Mock<UserManager<ApplicationUser>> mock_user_manager { get; set; }

        Mock<DbSet<Twit>> mock_twit_table { get; set; }
        Mock<DbSet<ApplicationUser>> mock_user_table { get; set; }
        Mock<DbSet<Tweet>> mock_tweet_table { get; set; }

        List<Twit> twit_list { get; set; }
        List<ApplicationUser> user_list { get; set;}
        List<Tweet> tweet_list { get; set; }

        TweeterRepo repo { get; set; }

        public void ConnectMocksToDatastore()
        {
            var queryable_twit_list = twit_list.AsQueryable();
            var queryable_user_list = user_list.AsQueryable();
            var queryable_tweet_list = tweet_list.AsQueryable();


            mock_twit_table.As<IQueryable<Twit>>().Setup(m => m.Provider).Returns(queryable_twit_list.Provider);
            mock_twit_table.As<IQueryable<Twit>>().Setup(m => m.Expression).Returns(queryable_twit_list.Expression);
            mock_twit_table.As<IQueryable<Twit>>().Setup(m => m.ElementType).Returns(queryable_twit_list.ElementType);
            mock_twit_table.As<IQueryable<Twit>>().Setup(m => m.GetEnumerator()).Returns(() => queryable_twit_list.GetEnumerator());

            mock_user_table.As<IQueryable<ApplicationUser>>().Setup(m => m.Provider).Returns(queryable_user_list.Provider);
            mock_user_table.As<IQueryable<ApplicationUser>>().Setup(m => m.Expression).Returns(queryable_user_list.Expression);
            mock_user_table.As<IQueryable<ApplicationUser>>().Setup(m => m.ElementType).Returns(queryable_user_list.ElementType);
            mock_user_table.As<IQueryable<ApplicationUser>>().Setup(m => m.GetEnumerator()).Returns(() => queryable_user_list.GetEnumerator());

            mock_tweet_table.As<IQueryable<Tweet>>().Setup(m => m.Provider).Returns(queryable_tweet_list.Provider);
            mock_tweet_table.As<IQueryable<Tweet>>().Setup(m => m.Expression).Returns(queryable_tweet_list.Expression);
            mock_tweet_table.As<IQueryable<Tweet>>().Setup(m => m.ElementType).Returns(queryable_tweet_list.ElementType);
            mock_tweet_table.As<IQueryable<Tweet>>().Setup(m => m.GetEnumerator()).Returns(() => queryable_tweet_list.GetEnumerator());

            mock_context.Setup(c => c.TweeterUsers).Returns(mock_twit_table.Object);
            mock_user_manager.Setup(c => c.Users).Returns(mock_user_table.Object);
            mock_context.Setup(c => c.Tweets).Returns(mock_tweet_table.Object);

            mock_user_table.Setup(t => t.Add(It.IsAny<ApplicationUser>())).Callback((ApplicationUser t) => user_list.Add(t));
            mock_twit_table.Setup(t => t.Add(It.IsAny<Twit>())).Callback((Twit t) => twit_list.Add(t));
            mock_tweet_table.Setup(t => t.Add(It.IsAny<Tweet>())).Callback((Tweet t) => tweet_list.Add(t));

            mock_twit_table.Setup(t => t.Remove(It.IsAny<Twit>())).Callback((Twit t) => twit_list.Remove(t));
            mock_tweet_table.Setup(t => t.Remove(It.IsAny<Tweet>())).Callback((Tweet t) => tweet_list.Remove(t));
        }

        [TestInitialize]
        public void Initialize()
        {
            mock_context = new Mock<TweeterContext>();
            mock_user_manager = new Mock<UserManager<ApplicationUser>>();

            mock_user_table = new Mock<DbSet<ApplicationUser>>();
            mock_twit_table = new Mock<DbSet<Twit>>();
            mock_tweet_table = new Mock<DbSet<Tweet>>();

            twit_list = new List<Twit>();
            user_list = new List<ApplicationUser>();
            tweet_list = new List<Tweet>();

            repo = new TweeterRepo(mock_context.Object);

            ConnectMocksToDatastore();
        }

        [TestCleanup]
        public void TearDown()
        {
            repo = null;
        }

        [TestMethod]
        public void TwitRepoOriginallyHasNoUsers()
        {
            //Arrange
            List<string> twits_usernames_returned = repo.GetAllUsernames();
            //Act
            int expected_response_count = 0;
            int actual_response_count = twits_usernames_returned.Count();
            //Assert
            Assert.AreEqual(expected_response_count, actual_response_count);
        }

        [TestMethod]
        public void TwitWillReturnBoolIfUserExistsInSystemAlready()
        {
            //Arrange
            //NEED TO ADD USER HERE
            ApplicationUser user = new ApplicationUser() { UserName = "morecallan", Email = "callan@MoreCallan.com" };
            repo.AddTwitToDatabase(user);

            //Act
            bool UserExists = repo.UsernameExists("morecallan");

            bool expected_result = true;
            bool actual_result = UserExists;
            //Assert
            Assert.AreEqual(expected_result, actual_result);
        }

        [TestMethod]
        public void TwitsCanBeFoundByApplicationUser()
        {
            //Arrange
            ApplicationUser user1 = new ApplicationUser() { UserName = "morecallan", Email = "callan@MoreCallan.com" };
            ApplicationUser user2 = new ApplicationUser() { UserName = "morecallan2", Email = "callan2@MoreCallan.com" };

            Twit twit1 = repo.AddTwitToDatabase(user1);
            twit1.TwitId = 1;
            Twit twit2 = repo.AddTwitToDatabase(user2);
            twit2.TwitId = 2;

            //Act
            Twit found_twit = repo.FindTwitBasedOnApplicationUser(user2);
            int expected_twit_id = 2;
            int actual_twit_id = found_twit.TwitId;

            //Assert
            Assert.AreEqual(expected_twit_id, actual_twit_id);
        }

        [TestMethod]
        public void TweetsCanBeReturnedById()
        {
            //Arrange
            Tweet tweet1 = new Tweet { TweetId = 1, Message = "Hello" };
            Tweet tweet2 = new Tweet { TweetId = 2, Message = "Goodbye" };
            Tweet tweet3 = new Tweet { TweetId = 3, Message = "Smarties" };


            //Act
            repo.AddTweet(tweet1);
            repo.AddTweet(tweet2);
            repo.AddTweet(tweet3);
            Tweet tweet_returned = repo.GetTweetById(2);

            string expected_tweet_message = "Goodbye";
            string actual_tweet_message = tweet_returned.Message;

            //Assert
            Assert.AreEqual(expected_tweet_message, actual_tweet_message);
        }

        [TestMethod]
        public void TweetsCanBeAddedToDatabase()
        {
            //Arrange
            Tweet tweet1 = new Tweet { TweetId = 1, Message = "Hello" };
            Tweet tweet2 = new Tweet { TweetId = 2, Message = "Goodbye" };
            Tweet tweet3 = new Tweet { TweetId = 3, Message = "Smarties" };


            //Act
            repo.AddTweet(tweet1);
            repo.AddTweet(tweet2);
            repo.AddTweet(tweet3);
            List<Tweet> tweets_returned = repo.GetAllTweets();

            int expected_tweet_count = 3;
            int actual_tweet_count = tweets_returned.Count();

            //Assert
            Assert.AreEqual(expected_tweet_count, actual_tweet_count);
        }

        [TestMethod]
        public void TweetsCanBeDeletedFromDatabaseByFullTweet()
        {
            //Arrange
            Tweet tweet1 = new Tweet { TweetId = 1, Message = "Hello" };
            Tweet tweet2 = new Tweet { TweetId = 2, Message = "Goodbye" };
            Tweet tweet3 = new Tweet { TweetId = 3, Message = "Smarties" };


            //Act
            repo.AddTweet(tweet1);
            repo.AddTweet(tweet2);
            repo.AddTweet(tweet3);

            repo.DeleteSpecificTweet(tweet2);
            List<Tweet> tweets_returned = repo.GetAllTweets();


            int expected_tweet_count = 2;
            int actual_tweet_count = tweets_returned.Count();

            //Assert
            Assert.AreEqual(expected_tweet_count, actual_tweet_count);
        }

        [TestMethod]
        public void TweetsCanBeDeletedFromDatabaseByTweetId()
        {
            //Arrange
            Tweet tweet1 = new Tweet { TweetId = 1, Message = "Hello" };
            Tweet tweet2 = new Tweet { TweetId = 2, Message = "Goodbye" };
            Tweet tweet3 = new Tweet { TweetId = 3, Message = "Smarties" };


            //Act
            repo.AddTweet(tweet1);
            repo.AddTweet(tweet2);
            repo.AddTweet(tweet3);

            repo.DeleteSpecificTweet(3);
            List<Tweet> tweets_returned = repo.GetAllTweets();


            int expected_tweet_count = 2;
            int actual_tweet_count = tweets_returned.Count();

            //Assert
            Assert.AreEqual(expected_tweet_count, actual_tweet_count);
        }

        [TestMethod]
        public void UserSpecificTweetsCanBeReturnedWithTwitId()
        {
            //Arrange
            Tweet tweet1 = new Tweet { TweetId = 1, Message = "Hello" };
            Tweet tweet2 = new Tweet { TweetId = 2, Message = "Goodbye" };
            Tweet tweet3 = new Tweet { TweetId = 3, Message = "Smarties" };

            ApplicationUser user1 = new ApplicationUser() { UserName = "morecallan", Email = "callan@MoreCallan.com" };
            ApplicationUser user2 = new ApplicationUser() { UserName = "morecallan2", Email = "callan2@MoreCallan.com" };

            Twit twit1 = repo.AddTwitToDatabase(user1);
            twit1.TwitId = 1;
            Twit twit2 = repo.AddTwitToDatabase(user2);
            twit2.TwitId = 2;

            tweet1.Author = twit1;
            tweet2.Author = twit1;
            tweet3.Author = twit2;

            repo.AddTweet(tweet1);
            repo.AddTweet(tweet2);
            repo.AddTweet(tweet3);

            //Act
            List<Tweet> user_tweets = repo.GetAllUserSpecificTweets(1);
            int expected_user_tweets_count = 2;
            int actual_user_tweets_count = user_tweets.Count();

            //Assert
            Assert.AreEqual(expected_user_tweets_count, actual_user_tweets_count);

        }
    }
    }
