using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Tweeter.DAL;
using Tweeter.Models;
using Moq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Tweeter.Tests.DAL
{
    [TestClass]
    public class TweeterRepositoryTest
    {
      
        //creating Mock Connections
        Mock<TweeterContext> mock_context { get; set; }
        Mock<DbSet<Twit>> mock_twit_table { get; set; }  //user id picked by class to represent author

        List<Twit> twit_list { get; set; }
        TweeterRepository repo { get; set; }
        List<ApplicationUser> user_list { get; set; }  //user id wass stored as email

        Mock<DbSet<ApplicationUser>> mock_users_table { get; set; }
        Mock<UserManager<ApplicationUser>> mock_user_mgr { get; set; }
       
        public void ConnectMocksToDataStore()
        {
            var queryable_twit_list = twit_list.AsQueryable();
            var query_users_list = user_list.AsQueryable();
            
            //Tricking LINQ to think list is a database table
            mock_users_table.As<IQueryable<ApplicationUser>>().Setup(m => m.Provider).Returns(query_users_list.Provider);
            mock_users_table.As<IQueryable<ApplicationUser>>().Setup(m => m.Expression).Returns(query_users_list.Expression);
            mock_users_table.As<IQueryable<ApplicationUser>>().Setup(m => m.ElementType).Returns(query_users_list.ElementType);
            mock_users_table.As<IQueryable<ApplicationUser>>().Setup(m => m.GetEnumerator()).Returns(() => query_users_list.GetEnumerator());
            
            //tricking LINQ to to think list is a database table
            mock_twit_table.As<IQueryable<Twit>>().Setup(m => m.Provider).Returns(queryable_twit_list.Provider);
            mock_twit_table.As<IQueryable<Twit>>().Setup(m => m.Expression).Returns(queryable_twit_list.Expression);
            mock_twit_table.As<IQueryable<Twit>>().Setup(m => m.ElementType).Returns(queryable_twit_list.ElementType);
            mock_twit_table.As<IQueryable<Twit>>().Setup(m => m.GetEnumerator()).Returns(() => queryable_twit_list.GetEnumerator());

            //response property returns fake database table
            mock_context.Setup(c => c.TweeterUsers).Returns(mock_twit_table.Object);
            mock_user_mgr.Setup(c => c.Users).Returns(mock_users_table.Object);

            //define callback for response to a called method
            mock_twit_table.Setup(t => t.Add(It.IsAny<Twit>())).Callback((Twit s) => twit_list.Add(s));
            mock_users_table.Setup(t => t.Add(It.IsAny<ApplicationUser>())).Callback((ApplicationUser s) => user_list.Add(s));
        }


        [TestInitialize] //runs before any tests
            public void Intialize()
            {
                //create
                mock_users_table = new Mock<DbSet<ApplicationUser>>();
                mock_context = new Mock<TweeterContext>();
                mock_twit_table = new Mock<DbSet<Twit>>();
                twit_list = new List<Twit>();  //fake database
                repo = new TweeterRepository(mock_context.Object);
                user_list = new List<ApplicationUser>();
                mock_user_mgr = new Mock<UserManager<ApplicationUser>>();

                ConnectMocksToDataStore();

            }

        [TestCleanup] //runs after every test
            public void TearDown()
            {
                repo = null; //reset repo 
            }

        [TestMethod]
            public void RepoEnsureCanCreateInstance()
            {
                TweeterRepository repo = new TweeterRepository();
                Assert.IsNotNull(repo);
            }

        [TestMethod]
        public void RepoEnsureRepoHasContext()
        {
            // Arange
            // Act
            TweeterContext actual_context = repo.Context;

            // Assert
            Assert.IsInstanceOfType(actual_context, typeof(TweeterContext));
        }

        [TestMethod]
            public void RepoEnsureIcanGetUserNames()
            {
                // Arrange
                //List<string> usernames_returned = repo.GetUserNames();
                ApplicationUser my_user = new ApplicationUser()
                {
                    UserName = "RadBrad",
                    Email = "radbrad@radbrad.com"
                };

                // Act
                repo.AddUserToDatabase(my_user);
                int actual_user_count = repo.GetUserNames().Count;
                int expected_user_count = 1;
            
                // Assert
                Assert.AreEqual(expected_user_count, actual_user_count);
            
            }

        [TestMethod]
            public void RepoEnsureUserNameDoesNotExist()
            {

                // Arrange
                ApplicationUser my_user = new ApplicationUser()
                {
                    UserName = "RadBrad",
                    Email = "radbrad@radbrad.com"

                };
                repo.AddUserToDatabase(my_user);

                // Act
                bool UserNameExist = repo.UsernameExists("RadBrad");
                bool expected_user_test = true;
                bool actual_user_test = UserNameExist;

                // Assert
                Assert.AreEqual(expected_user_test, actual_user_test);

            }
    }
}
