using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tweeter.DAL;
using Moq;
using Tweeter.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Tweeter.Tests
{
    [TestClass]
    public class TweeterRepoTests
    {
        Mock<TweeterContext> mock_context { get; set; }
        Mock<DbSet<Twit>> mock_twit_table { get; set; }
        List<Twit> twit_list { get; set; }
        TweeterRepo repo { get; set; }

        public void ConnectMocksToDatastore()
        {
            var queryable_list = twit_list.AsQueryable();

            mock_twit_table.As<IQueryable<Twit>>().Setup(m => m.Provider).Returns(queryable_list.Provider);
            mock_twit_table.As<IQueryable<Twit>>().Setup(m => m.Expression).Returns(queryable_list.Expression);
            mock_twit_table.As<IQueryable<Twit>>().Setup(m => m.ElementType).Returns(queryable_list.ElementType);
            mock_twit_table.As<IQueryable<Twit>>().Setup(m => m.GetEnumerator()).Returns(() => queryable_list.GetEnumerator());

            mock_context.Setup(c => c.TweeterUsers).Returns(mock_twit_table.Object);

            mock_twit_table.Setup(t => t.Add(It.IsAny<Twit>())).Callback((Twit t) => twit_list.Add(t));
        }

        [TestInitialize]
        public void Initialize()
        {
            mock_context = new Mock<TweeterContext>();
            mock_twit_table = new Mock<DbSet<Twit>>();
            twit_list = new List<Twit>();
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
            List<Twit> twits_returned = repo.GetAllUsernames();
            //Act
            int expected_response_count = 0;
            int actual_response_count = twits_returned.Count();
            //Assert
            Assert.AreEqual(expected_response_count, actual_response_count);
        }

        [TestMethod]
        public void TwitWillReturnBoolIfUserExistsInSystemAlready()
        {
            //Arrange
            bool UserExists = repo.UsernameExists("morecallan");
            //Act


            int expected_response_count = 0;
            int actual_result = twits_returned.Count();
            //Assert
            Assert.AreEqual(expected_response_count, actual_response_count);
        }

    }
