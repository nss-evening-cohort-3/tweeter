using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tweeter.DAL;
using Moq;
using Tweeter.Models;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;

namespace Tweeter.Tests
{
    [TestClass]
    public class TweeterRepoTests
    {

        private Mock<TweeterContext> MockContext;
        private Mock<DbSet<Twit>> TweeterUsers;
        private List<Twit> twits;

        [TestInitialize]
        public void Setup()
        {
            MockContext = new Mock<TweeterContext>();
            TweeterUsers = new Mock<DbSet<Twit>>();


            MockContext.Setup(x => x.TweeterUsers).Returns(TweeterUsers.Object);

            twits = new List<Twit>
            {
                new Twit
                {
                   Username = "user-1"
                },
                new Twit
                {
                   Username = "user-2"
                }
            };

            ConnectMocksToDatastore();
        }

        public void TestCleanup()
        {
            MockContext = null;
            TweeterUsers = null;
            twits = null;
        }


        public void ConnectMocksToDatastore()
        {
            var queryableTwits = twits.AsQueryable();
            TweeterUsers.As<IQueryable<Twit>>().Setup(x => x.Provider).Returns(queryableTwits.Provider);
            TweeterUsers.As<IQueryable<Twit>>().Setup(x => x.Expression).Returns(queryableTwits.Expression);
            TweeterUsers.As<IQueryable<Twit>>().Setup(x => x.ElementType).Returns(queryableTwits.ElementType);
            TweeterUsers.As<IQueryable<Twit>>().Setup(x => x.GetEnumerator()).Returns(queryableTwits.GetEnumerator());
        }



        [TestMethod]
        public void CanMakeInstanceOfRepo()
        {
            TweeterRepository myRepo = new TweeterRepository();
            Assert.IsNotNull(myRepo);
        }

        [TestMethod]
        public void EnsureMyRepoCanAccessContext()
        {
            TweeterRepository myRepo = new TweeterRepository(MockContext.Object);
        }

        [TestMethod]
        public void CanGetUsernames()
        {
            TweeterRepository myRepo = new TweeterRepository(MockContext.Object);
            List<Twit> myTwits = myRepo.GetUsernames();
            CollectionAssert.AreEqual(myTwits, twits);
        }

        [TestMethod]
        public void DoesUsernameExist()
        {
            TweeterRepository myRepo = new TweeterRepository(MockContext.Object);
            bool hasUsername = myRepo.UsernameExists("user-1");
            Assert.IsTrue(hasUsername);
        }
    }
}
