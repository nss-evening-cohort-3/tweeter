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

        private Mock<DbSet<ApplicationUser>> mock_users { get; set; }
        private Mock<TweeterContext> mock_context { get; set; }
        private TweeterRepository Repo { get; set; }
        private List<ApplicationUser> user_list { get; set; }

        [TestInitialize]
        public void Initialize()
        {
            mock_context = new Mock<TweeterContext>();
            mock_users = new Mock<DbSet<ApplicationUser>>();
            Repo = new TweeterRepository(mock_context.Object);
            ConnectToDatastore();
            /* 
             1. Install Identity into Tweeter.Tests (using statement needed)
             2. Create a mock context that uses 'UserManager' instead of 'TweeterContext'
             */
        }
        [TestCleanup]
        public void TearDown()
        {
            Repo = null;
        }

        public void ConnectToDatastore()
        {
            user_list = new List<ApplicationUser>();
            var queryable_list = user_list.AsQueryable();

            mock_users.As<IQueryable<ApplicationUser>>().Setup(m => m.Provider).Returns(queryable_list.Provider);
            mock_users.As<IQueryable<ApplicationUser>>().Setup(m => m.Expression).Returns(queryable_list.Expression);
            mock_users.As<IQueryable<ApplicationUser>>().Setup(m => m.ElementType).Returns(queryable_list.ElementType);
            mock_users.As<IQueryable<ApplicationUser>>().Setup(m => m.GetEnumerator()).Returns(() => queryable_list.GetEnumerator());

            /*
             * Below mocks the 'Users' getter that returns a list of ApplicationUsers
             * mock_user_manager_context.Setup(c => c.Users).Returns(mock_users.Object);
             * 
             */
            /* IF we just add a Username field to the Twit model*/

            mock_context.Setup(c => c.TweeterUsers).Returns(mock_users.Object); //Assuming mock_users is List<Twit>

            mock_users.Setup(t => t.Add(It.IsAny<ApplicationUser>())).Callback((ApplicationUser u) => queryable_list.Add(u));
            mock_users.Setup(t => t.Remove(It.IsAny<ApplicationUser>())).Callback((ApplicationUser u) => queryable_list.Remove(u));
        }

        [TestMethod]
        public void RepoEnsureCanCreateInstance()
        {
            TweeterRepository repo = new TweeterRepository();
            Assert.IsNotNull(repo);
        }

        [TestMethod]
        public void EnsureRepoHasContext()
        {
            TweeterRepository repo = new TweeterRepository();
            TweeterContext actual_context = repo.Context;
            Assert.IsInstanceOfType(actual_context, typeof(TweeterContext));
        }

        [TestMethod]
        public void RepoEnsureICanGetUsernames()
        {
            GetUsernames();
        }

    }
}
