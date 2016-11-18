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
        private Mock<DbSet<ApplicationUser>> mock_application_user { get; set; }
        private Mock<TweeterContext> mock_context { get; set; }
        private TweeterRepository Repo { get; set; }
        private List<Twit> users { get; set; }
        private List<ApplicationUser> application_users { get; set; }

        [TestInitialize]
        public void Initialize()
        {
            mock_context = new Mock<TweeterContext>();
            mock_users = new Mock<DbSet<Twit>>();
            mock_application_user = new Mock<DbSet<ApplicationUser>>();
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
            application_users = new List<ApplicationUser>
            {
                new ApplicationUser
                {
                    Id = "1",
                    UserName = "michealb"
                },
                new ApplicationUser
                {
                    Id = "2",
                    UserName = "sallym"
                }
            };
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


            var query_application_users = application_users.AsQueryable();

            mock_application_user.As<IQueryable<ApplicationUser>>().Setup(m => m.Provider).Returns(query_application_users.Provider);
            mock_application_user.As<IQueryable<ApplicationUser>>().Setup(m => m.Expression).Returns(query_application_users.Expression);
            mock_application_user.As<IQueryable<ApplicationUser>>().Setup(m => m.ElementType).Returns(query_application_users.ElementType);
            mock_application_user.As<IQueryable<ApplicationUser>>().Setup(m => m.GetEnumerator()).Returns(() => query_application_users.GetEnumerator());

            mock_context.Setup(c => c.Users).Returns(mock_application_user.Object);
            mock_application_user.Setup(u => u.Add(It.IsAny<ApplicationUser>())).Callback((ApplicationUser t) => application_users.Add(t));
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
        public void RepoCanIFindATwitUser()
        {
            // Arrange
            ConnectToDatastore();

            // Act
            Twit found_twit = Repo.FindTwitUser("1");

            // Assert
            Assert.AreEqual("michealb", found_twit.BaseUser.UserName);
        }
    }
}
