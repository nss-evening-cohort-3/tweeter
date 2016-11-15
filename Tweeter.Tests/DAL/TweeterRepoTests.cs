﻿using System;
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

        private Mock<DbSet<ApplicationUser>> mock_users { get; set; }

        private Mock<TweeterContext> mock_context { get; set; }
        private Mock<UserManager<ApplicationUser>> mock_usermanager_context { get; set; }
        private TweeterRepository Repo { get; set; }

        List<ApplicationUser> users = new List<ApplicationUser>()
            {
                new ApplicationUser {UserName = "Batman" }
            };


[TestInitialize]
        public void Initialize()
        {
            mock_usermanager_context = new Mock<UserManager<ApplicationUser>>();
            mock_context = new Mock<TweeterContext>();
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
            var query_users = users.AsQueryable();

            mock_users.As<IQueryable<ApplicationUser>>().Setup(m => m.Provider).Returns(query_users.Provider);
            mock_users.As<IQueryable<ApplicationUser>>().Setup(m => m.Expression).Returns(query_users.Expression);
            mock_users.As<IQueryable<ApplicationUser>>().Setup(m => m.ElementType).Returns(query_users.ElementType);
            mock_users.As<IQueryable<ApplicationUser>>().Setup(m => m.GetEnumerator()).Returns(() => query_users.GetEnumerator());


            mock_context.Setup(c => c.Users).Returns(mock_users.Object);

            mock_users.Setup(t => t.Add(It.IsAny<ApplicationUser>())).Callback((ApplicationUser person) =>users.Add(person));
            /*
             * Below mocks the 'Users' getter that returns a list of ApplicationUsers
             * mock_user_manager_context.Setup(c => c.Users).Returns(mock_users.Object);
             * 
             */

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
            var repo = new TweeterRepository(mock_context.Object);

            var allUsernames = repo.GetUsernames();

            Assert.AreEqual(allUsernames.Count, 1);
        }

        [TestMethod]
        public void RepoEnsureICanGetByUsernames()
        {
            var repo = new TweeterRepository(mock_context.Object);

            var newUser = new ApplicationUser() { UserName = "Superman" };

            users.Add(newUser);

            var foundUser = repo.GetUsernames("Superman");

            Assert.IsTrue(users.Contains(foundUser));
        }
    }
}
