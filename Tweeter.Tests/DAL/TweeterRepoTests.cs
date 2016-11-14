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
        private Mock<TweeterContext> mock_context { get; set; }
        private Mock<DbSet<ApplicationUser>> mock_users { get; set; }
        private TweeterRepository Repo { get; set; }
        private Mock<UserManager<ApplicationUser>> mock_user_manager_context { get; set; }
        private List<ApplicationUser> users { get; set; }

        [TestInitialize]
        public void Initialize()
        {
            mock_context = new Mock<TweeterContext>();
            mock_users = new Mock<DbSet<ApplicationUser>>();
            Repo = new TweeterRepository(mock_context.Object);
            mock_user_manager_context = new Mock<UserManager<ApplicationUser>>();
            users = new List<ApplicationUser>();

            ConnectToDatastore();
        }

        public void ConnectToDatastore()
        {
            var query_users = users.AsQueryable();

            mock_users.As<IQueryable<ApplicationUser>>().Setup(m => m.Provider).Returns(query_users.Provider);
            mock_users.As<IQueryable<ApplicationUser>>().Setup(m => m.Expression).Returns(query_users.Expression);
            mock_users.As<IQueryable<ApplicationUser>>().Setup(m => m.ElementType).Returns(query_users.ElementType);
            mock_users.As<IQueryable<ApplicationUser>>().Setup(m => m.GetEnumerator()).Returns(() => query_users.GetEnumerator());

            mock_context.Setup(m => m.Users).Returns(mock_users.Object);

            mock_users.Setup(t => t.Add(It.IsAny<ApplicationUser>())).Callback((ApplicationUser t) => users.Add(t));
        }

        [TestMethod]
        public void RepoEnsureCanCreateInstance()
        {
            Assert.IsNotNull(Repo);
        }

        [TestMethod]
        public void RepoEnsureICanGetUsernames()
        {
            ApplicationUser testUser = new ApplicationUser() { UserName = "TestUser" };
            users.Add(testUser);
            
            var actual_user_names = Repo.GetUserNames();

            Assert.IsTrue(actual_user_names.Contains("TestUser"));
        }
        [TestMethod]
        public void RepoCanCheckIfUserNameExistsForUsers()
        {
            ApplicationUser testUser = new ApplicationUser() { UserName = "TestUser" };
            users.Add(testUser);

            Assert.AreEqual(true, Repo.CheckForUserName("TestUser"));
        }
        [TestMethod]
        public void RepoCheckForUserNameReturnsFalseIfUserNameDoesNotExist()
        {
            ApplicationUser testUser = new ApplicationUser() { UserName = "TestUser" };
            users.Add(testUser);

            Assert.AreEqual(false, Repo.CheckForUserName("BadName"));
        }
    }
}
