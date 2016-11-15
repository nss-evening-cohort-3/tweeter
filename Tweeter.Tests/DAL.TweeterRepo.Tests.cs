using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tweeter.DAL;
using Moq;
using Tweeter.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Tweeter.Tests
{
    [TestClass]
    public class TweeterRepoTests
    {
        Mock<TweeterContext> mock_context { get; set; }
        Mock<UserManager<ApplicationUser>> mock_user_manager { get; set; }

        Mock<DbSet<Twit>> mock_twit_table { get; set; }
        Mock<DbSet<ApplicationUser>> mock_user_table { get; set; }

        List<Twit> twit_list { get; set; }
        List<ApplicationUser> user_list { get; set;}

        TweeterRepo repo { get; set; }

        public void ConnectMocksToDatastore()
        {
            var queryable_twit_list = twit_list.AsQueryable();
            var queryable_user_list = user_list.AsQueryable();


            mock_twit_table.As<IQueryable<Twit>>().Setup(m => m.Provider).Returns(queryable_twit_list.Provider);
            mock_twit_table.As<IQueryable<Twit>>().Setup(m => m.Expression).Returns(queryable_twit_list.Expression);
            mock_twit_table.As<IQueryable<Twit>>().Setup(m => m.ElementType).Returns(queryable_twit_list.ElementType);
            mock_twit_table.As<IQueryable<Twit>>().Setup(m => m.GetEnumerator()).Returns(() => queryable_twit_list.GetEnumerator());

            mock_user_table.As<IQueryable<ApplicationUser>>().Setup(m => m.Provider).Returns(queryable_user_list.Provider);
            mock_user_table.As<IQueryable<ApplicationUser>>().Setup(m => m.Expression).Returns(queryable_user_list.Expression);
            mock_user_table.As<IQueryable<ApplicationUser>>().Setup(m => m.ElementType).Returns(queryable_user_list.ElementType);
            mock_user_table.As<IQueryable<ApplicationUser>>().Setup(m => m.GetEnumerator()).Returns(() => queryable_user_list.GetEnumerator());

            mock_context.Setup(c => c.TweeterUsers).Returns(mock_twit_table.Object);
            mock_user_manager.Setup(c => c.Users).Returns(mock_user_table.Object);

            mock_user_table.Setup(t => t.Add(It.IsAny<ApplicationUser>())).Callback((ApplicationUser t) => user_list.Add(t));
            mock_twit_table.Setup(t => t.Add(It.IsAny<Twit>())).Callback((Twit t) => twit_list.Add(t));
        }

        [TestInitialize]
        public void Initialize()
        {
            mock_context = new Mock<TweeterContext>();
            mock_user_manager = new Mock<UserManager<ApplicationUser>>();

            mock_user_table = new Mock<DbSet<ApplicationUser>>();
            mock_twit_table = new Mock<DbSet<Twit>>();

            twit_list = new List<Twit>();
            user_list = new List<ApplicationUser>();

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
            List<string> twits_usernames_returned = repo.GetAllUsernames();
            //Act
            int expected_response_count = 0;
            int actual_response_count = twits_usernames_returned.Count();
            //Assert
            Assert.AreEqual(expected_response_count, actual_response_count);
        }

        [TestMethod]
        public void TwitWillReturnBoolIfUserExistsInSystemAlready()
        {
            //Arrange
            //NEED TO ADD USER HERE
            ApplicationUser user = new ApplicationUser() { UserName = "morecallan", Email = "callan@MoreCallan.com" };
            repo.AddTwitToDatabase(user);

            //Act
            bool UserExists = repo.UsernameExists("morecallan");

            bool expected_result = true;
            bool actual_result = UserExists;
            //Assert
            Assert.AreEqual(expected_result, actual_result);
        }
    }
    }
