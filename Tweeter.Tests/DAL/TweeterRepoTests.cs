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

        private Mock<TweeterContext> mock_context { get; set; }
        private Mock<DbSet<Twit>> mock_users { get; set; }
        private List<Twit> users { get; set; }
        private TweeterRepository repo { get; set; }

        public void ConnectToDatastore()
        {
            var query_users = users.AsQueryable();
            ApplicationUser bob = new ApplicationUser { UserName = "bob",  };
            mock_users.As<IQueryable<Twit>>().Setup(m => m.Provider).Returns(query_users.Provider);
            mock_users.As<IQueryable<Twit>>().Setup(m => m.Expression).Returns(query_users.Expression);
            mock_users.As<IQueryable<Twit>>().Setup(m => m.ElementType).Returns(query_users.ElementType);
            mock_users.As<IQueryable<Twit>>().Setup(m => m.GetEnumerator()).Returns(() => query_users.GetEnumerator());

            mock_context.Setup(m => m.TweeterUsers).Returns(mock_users.Object);

            mock_users.Setup(t => t.Add(It.IsAny<Twit>())).Callback((Twit u) => users.Add(u));

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
            mock_users = new Mock<DbSet<Twit>>();
            repo = new TweeterRepository(mock_context.Object);
            users = new List<Twit>();
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
        public void RepoEnsureCanCreateInstance()
        {
            TweeterRepository repo = new TweeterRepository();
            Assert.IsNotNull(repo);
        }

        [TestMethod]
        public void EnsureCanAddUser()
        {
            Twit bob = new Twit { TwitName = "Bob" };
            repo.AddUser(bob);
            int expected_count = 1;
            int actual_count = repo.GetUsers().Count();
            Assert.AreEqual(expected_count, actual_count);
        }
        [TestMethod]
        public void EnsureCanGetUserByName()
        {
            Twit bob = new Twit { TwitName = "Bob" };
            Twit dave = new Twit { TwitName = "Dave" };
            repo.AddUser(bob);
            repo.AddUser(dave);
            Twit actual = repo.GetUserByName("bob");
            string expected = "bob";
            Assert.AreEqual(expected, actual.TwitName.ToLower());

        }
    }
}
