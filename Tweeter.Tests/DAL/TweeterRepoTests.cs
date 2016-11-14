using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tweeter.DAL;
using System.Collections.Generic;
using Tweeter.Models;
using Moq;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Tweeter.Tests.DAL
{
    [TestClass]
    public class TweeterRepoTests
    {
        private Mock<TweeterContext> mock_context { get; set; }
        private Mock<DbSet<ApplicationUser>> mock_users { get; set; }
        private List<ApplicationUser> user_list { get; set; } // Fake
        private TweeterRepository repo { get; set; }

        public void ConnectMocksToDatastore()
        {
            var queryable_list = user_list.AsQueryable();

            // Lie to LINQ make it think that our new Queryable List is a Database table.
            mock_users.As<IQueryable<ApplicationUser>>().Setup(m => m.Provider).Returns(queryable_list.Provider);
            mock_users.As<IQueryable<ApplicationUser>>().Setup(m => m.Expression).Returns(queryable_list.Expression);
            mock_users.As<IQueryable<ApplicationUser>>().Setup(m => m.ElementType).Returns(queryable_list.ElementType);
            mock_users.As<IQueryable<ApplicationUser>>().Setup(m => m.GetEnumerator()).Returns(() => queryable_list.GetEnumerator());

            // Have our User property return our Queryable List AKA Fake database table.
            mock_context.Setup(c => c.TweeterUsers).Returns(mock_users);

            mock_users.Setup(t => t.Add(It.IsAny<ApplicationUser>())).Callback((ApplicationUser a) => user_list.Add(a));
            mock_users.Setup(t => t.Remove(It.IsAny<ApplicationUser>())).Callback((ApplicationUser a) => user_list.Remove(a));
        }

        [TestInitialize]
        public void Initialize()
        {
            // Create Mock TweeterContext
            mock_context = new Mock<TweeterContext>();
            user_list = new List<ApplicationUser>(); // Fake
            repo = new TweeterRepository(mock_context.Object);

            ConnectMocksToDatastore();
        }

        [TestCleanup]
        public void TearDown()
        {
            repo = null;
        }

        [TestMethod]
        public void RepoEnsureCanCreateInstance()
        {
            TweeterRepository repo = new TweeterRepository();
            Assert.IsNotNull(repo);
        }

        [TestMethod]
        public void RepoEnsureRepoHasContext()
        {
            TweeterRepository repo = new TweeterRepository();

            TweeterContext actual_context = repo.Context;

            Assert.IsInstanceOfType(actual_context, typeof(TweeterContext));
        }

        [TestMethod]
        public void RepoEnsureWeHaveNoUsers()
        {
            List<Twit> actual_users = repo.GetUsernames();
            int expected_users_count = 0;
            int actual_users_count = actual_users.Count;

            Assert.AreEqual(expected_users_count, actual_users_count);
        }

        [TestMethod] 
        public void RepoEnsureAddUserToDatabase()
        {
            // var user = new ApplicationUser { UserName =  };
            ApplicationUser my_user = new ApplicationUser { UserName = "user" };  // What the actual fuck?

            repo.AddUser(my_user);

            int actual_user_count = repo.GetUsernames().Count;

            int expected_user_count = 1;

            Assert.AreEqual(expected_user_count, actual_user_count);
        }

        [TestMethod]
        public void RepoEnsureAddUserWithArgs()
        {
            repo.AddUser();  // What the actual fuck?

            List<Twit> actual_users = repo.GetUsernames();
            string actual_username = actual_users.First();
            string expected_username = "Voldemort";

            Assert.AreEqual(expected_username, actual_username);
        }

        [TestMethod]
        public void RepoEnsureFindUserByUsername()
        {
            user_list.Add(new Twit { BaseUser = 1 });
            user_list.Add(new Twit { BaseUser = 2 });
            user_list.Add(new Twit { BaseUser = 3 });

            string username = "BadAss";
            Twit actual_user = repo.FindUserByUsername(username);

            int expected_user_id = 1;
            int actual_user_id = actual_user.TwitId;
            Assert.AreEqual(expected_user_id, actual_user_id);
        }

    }
}
