using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tweeter.DAL;
using Moq;
using System.Data.Entity;
using Tweeter.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity;

namespace Tweeter.Tests.DAL
{
    [TestClass]
    public class TweeterRepoTests
    {

        private Mock<DbSet<Twit>> mock_users { get; set; }
        private Mock<DbSet<Tweet>> mock_tweets { get; set; }
        private Mock<DbSet<Follow>> mock_follows { get; set; }

        private Mock<DbSet<ApplicationUser>> mock_app_users { get; set; }
        private Mock<TweeterContext> mock_context { get; set; }
        private TweeterRepository Repo { get; set; }
        private List<Twit> users { get; set; }
        private List<Twit> follow_users { get; set; }
        private List<Tweet> tweets { get; set; }
        private List<Follow> follows { get; set; }

        private Mock<UserManager<ApplicationUser>> mock_user_manager_context {get; set;}
        private List<ApplicationUser> app_users { get; set; }


        [TestInitialize]
        public void Initialize()
        {
            mock_context = new Mock<TweeterContext>();
            mock_users = new Mock<DbSet<Twit>>();
            mock_tweets = new Mock<DbSet<Tweet>>();
            mock_follows = new Mock<DbSet<Follow>>();
            mock_app_users = new Mock<DbSet<ApplicationUser>>();
            mock_user_manager_context = new Mock<UserManager<ApplicationUser>>();
            Repo = new TweeterRepository(mock_context.Object);

            tweets = new List<Tweet>();
            follow_users = new List<Twit>();
            follows = new List<Follow>();
            ApplicationUser sallym = new ApplicationUser { Email = "sally@example.com", UserName = "sallym", Id = "1234567" };
            ApplicationUser michealb = new ApplicationUser { Email = "micheal@example.com", UserName = "michealb", Id = "1234568" };
            app_users = new List<ApplicationUser>()
            {
                sallym,
                michealb
            };

            users = new List<Twit>
            {
                new Twit {
                    TwitId = 1,
                    BaseUser = michealb
                },
                new Twit {
                    TwitId = 2,
                    BaseUser = sallym
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
            var query_app_users = app_users.AsQueryable();
            var query_tweets = tweets.AsQueryable();
            var query_follows = follows.AsQueryable();



            mock_users.As<IQueryable<Twit>>().Setup(m => m.Provider).Returns(query_users.Provider);
            mock_users.As<IQueryable<Twit>>().Setup(m => m.Expression).Returns(query_users.Expression);
            mock_users.As<IQueryable<Twit>>().Setup(m => m.ElementType).Returns(query_users.ElementType);
            mock_users.As<IQueryable<Twit>>().Setup(m => m.GetEnumerator()).Returns(() => query_users.GetEnumerator());

            mock_context.Setup(c => c.TweeterUsers).Returns(mock_users.Object);
            mock_users.Setup(u => u.Add(It.IsAny<Twit>())).Callback((Twit t) => users.Add(t));

            //mock_users.Setup( f => f.).Returns(mock_follow_users.Object); // Some list to contain fake users

            /*
             * Below mocks the 'Users' getter that returns a list of ApplicationUsers
             * mock_user_manager_context.Setup(c => c.Users).Returns(mock_users.Object);
             * 
             */
            mock_app_users.As<IQueryable<ApplicationUser>>().Setup(m => m.Provider).Returns(query_app_users.Provider);
            mock_app_users.As<IQueryable<ApplicationUser>>().Setup(m => m.Expression).Returns(query_app_users.Expression);
            mock_app_users.As<IQueryable<ApplicationUser>>().Setup(m => m.ElementType).Returns(query_app_users.ElementType);
            mock_app_users.As<IQueryable<ApplicationUser>>().Setup(m => m.GetEnumerator()).Returns(() => query_app_users.GetEnumerator());
            mock_context.Setup(c => c.Users).Returns(mock_app_users.Object);

            
            

            /* IF we just add a Username field to the Twit model
             * mock_context.Setup(c => c.TweeterUsers).Returns(mock_users.Object); Assuming mock_users is List<Twit>
             */

            mock_tweets.As<IQueryable<Tweet>>().Setup(m => m.Provider).Returns(query_tweets.Provider);
            mock_tweets.As<IQueryable<Tweet>>().Setup(m => m.Expression).Returns(query_tweets.Expression);
            mock_tweets.As<IQueryable<Tweet>>().Setup(m => m.ElementType).Returns(query_tweets.ElementType);
            mock_tweets.As<IQueryable<Tweet>>().Setup(m => m.GetEnumerator()).Returns(() => query_tweets.GetEnumerator());

            mock_context.Setup(c => c.Tweets).Returns(mock_tweets.Object);
            mock_tweets.Setup(u => u.Add(It.IsAny<Tweet>())).Callback((Tweet t) => tweets.Add(t));
            mock_tweets.Setup(u => u.Remove(It.IsAny<Tweet>())).Callback((Tweet t) => tweets.Remove(t));

            mock_follows.As<IQueryable<Follow>>().Setup(m => m.Provider).Returns(query_follows.Provider);
            mock_follows.As<IQueryable<Follow>>().Setup(m => m.Expression).Returns(query_follows.Expression);
            mock_follows.As<IQueryable<Follow>>().Setup(m => m.ElementType).Returns(query_follows.ElementType);
            mock_follows.As<IQueryable<Follow>>().Setup(m => m.GetEnumerator()).Returns(() => query_follows.GetEnumerator());

            mock_context.Setup(c => c.AllFollows).Returns(mock_follows.Object);
            mock_follows.Setup(u => u.Add(It.IsAny<Follow>())).Callback((Follow t) => follows.Add(t));
            mock_follows.Setup(u => u.Remove(It.IsAny<Follow>())).Callback((Follow t) => follows.Remove(t));


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
        public void RepoEnsureICanCreateTweet()
        {
            // Arrange
            ConnectToDatastore();

            // Act
            Tweet a_tweet = new Tweet {
                TweetId = 1,
                Message = "my message",
                Author = new Twit { TwitId = 1, BaseUser = new ApplicationUser { UserName = "jcockhren" } },
                CreatedAt = DateTime.Now
            };
            Repo.AddTweet(a_tweet);

            int expected_tweets = 1;
            int actual_tweets = Repo.Context.Tweets.Count();

            // Assert
            Assert.AreEqual(expected_tweets, actual_tweets);
        }

        [TestMethod]
        public void RepoEnsureICanCreateTweetWithMessage()
        {
            // Arrange
            ConnectToDatastore();

            // Act
            Repo.AddTweet("sallym", "my tweet!!!!");

            int expected_tweets = 1;
            int actual_tweets = Repo.Context.Tweets.Count();

            // Assert
            Assert.AreEqual(expected_tweets, actual_tweets);
        }

        [TestMethod]
        public void RepoEnsureICanRemoveTweet()
        {
            // Arrange
            ConnectToDatastore();
            Tweet a_tweet = new Tweet
            {
                TweetId = 3,
                Message = "my message",
                CreatedAt = DateTime.Now,
                Author = new Twit { TwitId = 3, BaseUser = new ApplicationUser { UserName = "jcockhren" } }
            };
            Tweet another_tweet = new Tweet
            {
                TweetId = 4,
                Message = "my message",
                CreatedAt = DateTime.Now,
                Author = new Twit { TwitId = 4, BaseUser = new ApplicationUser { UserName = "sallym" } }
            };
            Repo.AddTweet(a_tweet);
            Repo.AddTweet(another_tweet);

            // Act

            Tweet removed_tweet = Repo.RemoveTweet(3);

            int expected_tweets = 1;
            int actual_tweets = Repo.Context.Tweets.Count();

            // Assert
            Assert.AreEqual(expected_tweets, actual_tweets);
            Assert.IsNotNull(removed_tweet);
        }

        [TestMethod]
        public void RepoEnsureICanGetTweets()
        {
            // Arrange
            ConnectToDatastore();
            Repo.AddTweet("sallym", "my tweet!!!!");
            
            // Act

            int expected_tweets = 1;
            int actual_tweets = Repo.GetTweets().Count();

            // Assert
            Assert.AreEqual(expected_tweets, actual_tweets);
        }

        [TestMethod]
        public void RepoGetTwitUserFromUserID()
        {
            // Arrange
            ConnectToDatastore();

            // Act
            string user_id = "blah-blah-blah";
            Twit found_user = Repo.GetTwitUser(user_id);

            //Assert
            Assert.IsNotNull(found_user);
            Assert.AreEqual(user_id, found_user.BaseUser.Id);

        }

        [TestMethod]
        public void RepoEnsureTwitUserCannotFollowSelf()
        {
            // Arrange
            ConnectToDatastore();

            // Act
            string current_user = "sallym";
            string user_to_follow = current_user;
            bool follow_successful = Repo.FollowUser(current_user, user_to_follow);

            // Assert
            Assert.IsFalse(follow_successful);
        }

        [TestMethod]
        public void RepoEssureTwitUserCannotFollowNonExistentUser()
        {
            // Arrange
            ConnectToDatastore();

            // Act
            string current_user = "sallym";
            string user_to_follow = "darthvader";
            bool follow_successful = Repo.FollowUser(current_user, user_to_follow);

            // Assert
            Assert.IsFalse(follow_successful);
        }

        [TestMethod]
        public void RepoEnsureTwitUserCanFollow()
        {
            // Arrange
            ConnectToDatastore();

            // Act
            string current_user = "sallym";
            string user_to_follow = "michealb";
            int expected_follows_count = 1;
            bool follow_successful = Repo.FollowUser(current_user, user_to_follow);

            // We could extract this into it's own repo method.
            int actual_follow_count = Repo.Context.AllFollows.Count(
                f => f.TwitFollowed.BaseUser.UserName == user_to_follow && f.TwitFollower.BaseUser.UserName == current_user
            );

            // Assert
            Assert.IsTrue(follow_successful);
            Assert.AreEqual(expected_follows_count, actual_follow_count);
        }

        [TestMethod]
        public void RepoEnsureTwitUserCanUnFollow()
        {
            // Arrange
            follows.Add(
                new Follow
                {
                    FollowId = 1,
                    TwitFollowed = users.First(),
                    TwitFollower = users.Last()
                }
            );
            ConnectToDatastore();

            // Act
            string current_user = "michealb";
            string user_to_unfollow = "sallym";
            int expected_follows_count = 0;
            bool unfollow_successful = Repo.UnfollowUser(current_user, user_to_unfollow);
            int actual_follow_count = Repo.Context.AllFollows.Count(
                f => f.TwitFollowed.BaseUser.UserName == user_to_unfollow && f.TwitFollower.BaseUser.UserName == current_user
            );

            // Assert
            Assert.IsTrue(unfollow_successful);
            Assert.AreEqual(expected_follows_count, actual_follow_count);
        }
    }
}
