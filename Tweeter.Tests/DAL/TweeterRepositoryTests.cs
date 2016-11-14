using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweeter.DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tweeter.Models;
using Moq;
using System.Data.Entity;

namespace Tweeter.Tests.DAL
{
    [TestClass]
    public class TweeterRepositoryTests
    {
        
        private Mock<DbSet<ApplicationUser>> mock_users { get; set; }
        private Mock<TweeterContext> mock_context { get; set; }
        private TweeterRepository Repo { get; set; }
        private List<ApplicationUser> users { get; set; }

        [TestInitialize]
        public void Initialize()
        {
            mock_context = new Mock<TweeterContext>();
            mock_users = new Mock<DbSet<ApplicationUser>>();
            Repo = new TweeterRepository(mock_context.Object);
        }

        public void ConnectToDatastore()
        {
            users = new List<ApplicationUser>();
            var query_users = users.AsQueryable();

            mock_users.As<IQueryable<ApplicationUser>>().Setup(m => m.Provider).Returns(query_users.Provider);
            mock_users.As<IQueryable<ApplicationUser>>().Setup(m => m.Expression).Returns(query_users.Expression);
            mock_users.As<IQueryable<ApplicationUser>>().Setup(m => m.ElementType).Returns(query_users.ElementType);
            mock_users.As<IQueryable<ApplicationUser>>().Setup(m => m.GetEnumerator()).Returns(() =>query_users.GetEnumerator());

            mock_context.Setup(c => c.TweeterUsers).Returns(/*Tweeter User List*/);

        }

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

        }

        [TestMethod]
        public void GetListOfTwits()
        {
            TweeterRepository tr = new TweeterRepository();
            List<Twit> t = new List<Twit>();
            t = tr.GetUserNames();
            t.ToString();
        }
    }
}
