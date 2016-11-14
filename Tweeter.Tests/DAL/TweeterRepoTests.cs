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
        private TweeterRepository Repo { get; set; }

        public void ConnectToDatastore()
        {
            IQueryable<Twit> query_users = users.AsQueryable();

            mock_users.As<IQueryable<Twit>>().Setup(m => m.Provider).Returns(query_users.Provider);
            mock_users.As<IQueryable<Twit>>().Setup(m => m.Expression).Returns(query_users.Expression);
            mock_users.As<IQueryable<Twit>>().Setup(m => m.ElementType).Returns(query_users.ElementType);
            mock_users.As<IQueryable<Twit>>().Setup(m => m.GetEnumerator()).Returns(() => query_users.GetEnumerator());

            mock_context.Setup(c => c.TweeterUsers).Returns(mock_users.Object);
            mock_users.Setup(t => t.Add(It.IsAny<Twit>())).Callback((Twit a) => users.Add(a));
        }

        [TestInitialize]
        public void Initialize()
        {
            mock_context = new Mock<TweeterContext>();
            mock_users = new Mock<DbSet<Twit>>();
            Repo = new TweeterRepository(mock_context.Object);

            users = new List<Twit>()
                {
                    new Twit
                    {
                        TwitId = 0,
                        BaseUser = new ApplicationUser() { UserName = "John Jackson" }
                    },
                   new Twit
                    {
                        TwitId = 1,
                        BaseUser = new ApplicationUser() { UserName = "Jack Johnson" }
                    },
                };

            ConnectToDatastore();
        }

        [TestMethod]
        public void RepoEnsureCanCreateInstance()
        {
            TweeterRepository repo = new TweeterRepository();
            Assert.IsNotNull(repo);
        }

        [TestMethod]
        public void RepoEnsureICanGetCorrectNumberOfUsernames()
        {
            List<string> test = Repo.GetUsernames();
            Assert.AreEqual(2, test.Count);
        }

        [TestMethod]
        public void RepoEnsureICanFindAUsername()
        {
            Assert.IsTrue(Repo.UsernameExists("John Jackson"));
            Assert.IsTrue(Repo.UsernameExists("Jack Johnson"));
        }

        [TestMethod]
        public void RepoEnsureICantFindAUsernameNotPresent()
        {
            Assert.IsFalse(Repo.UsernameExists("Billy Bob Jackson"));
        }
    }
}
