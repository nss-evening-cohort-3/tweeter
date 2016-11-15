﻿using System;
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
        private Mock<TweeterContext> mock_user_manager_context { get; set; }
        private TweeterRepository Repo { get; set; }
        private List<ApplicationUser> users { get; set; }

        [TestInitialize]
        public void Initialize()
        {
            mock_context = new Mock<TweeterContext>();
            mock_user_manager_context = new Mock<TweeterContext>();
            mock_users = new Mock<DbSet<ApplicationUser>>();
            Repo = new TweeterRepository(mock_context.Object);
            ConnectToDatastore();
            /* 
             1. Install Identity into Tweeter.Tests (using statement needed)
             2. Create a mock context that uses 'UserManager' instead of 'TweeterContext'
             */
        }

        public void ConnectToDatastore()
        {
            users = new List<ApplicationUser>();
            var query_users = users.AsQueryable();

            mock_users.As<IQueryable<ApplicationUser>>().Setup(m => m.Provider).Returns(query_users.Provider);
            mock_users.As<IQueryable<ApplicationUser>>().Setup(m => m.Expression).Returns(query_users.Expression);
            mock_users.As<IQueryable<ApplicationUser>>().Setup(m => m.ElementType).Returns(query_users.ElementType);
            mock_users.As<IQueryable<ApplicationUser>>().Setup(m => m.GetEnumerator()).Returns(() => query_users.GetEnumerator());

            /*
             * Below mocks the 'Users' getter that returns a list of ApplicationUsers
             
             * 
             */

            mock_user_manager_context.Setup(c => c.Users).Returns(mock_users.Object);

            /* IF we just add a Username field to the Twit model
             * mock_context.Setup(c => c.TweeterUsers).Returns(mock_users.Object); Assuming mock_users is List<Twit>
             */
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
        
            var user1 = new ApplicationUser { UserName = "James123", Email = "james123@email.com" };
            users.Add(user1);


            TweeterRepository repo = new TweeterRepository(mock_user_manager_context.Object);
            List<string> all_users = repo.GetAllAppUsers();
            
            

            int expected_count = 1;
            int actual_count = all_users.Count();

            Assert.AreEqual(expected_count, actual_count);
            Assert.IsInstanceOfType(all_users, typeof(List<string>));
        }

        [TestMethod]
        public void RepoEnsureCanVerifyUsernameExists()
        {
            TweeterRepository repo = new TweeterRepository(mock_user_manager_context.Object);
            var user1 = new ApplicationUser { UserName = "James123", Email = "james123@email.com" };
            users.Add(user1);
            bool usernameExists = repo.CheckIfUsernameExists("James123");


            Assert.IsTrue(usernameExists);

        }
    }
}
