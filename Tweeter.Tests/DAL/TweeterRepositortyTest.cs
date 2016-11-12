using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Tweeter.DAL;
using Tweeter.Models;
using Moq;

namespace Tweeter.Tests.DAL
{
    [TestClass]
    public class TweeterRepositoryTest
    {
      
        //creating Mock Connections
        Mock<TweeterContext> mock_context { get; set; }
        Mock<DbSet<Tweet>> mock_tweet_table { get; set; }
        List<Tweet> tweeter_list { get; set; }
        TweeterRepository repo { get; set; }
        List<ApplicationUser> user { get; set; }
        Mock<DbSet<ApplicationUser>> mock_users { get; set; }

        /*
         
             
             
             
             
        */






        public void ConnectMocksToDataStore()
        {
            var queryable_list = tweeter_list.AsQueryable();
            var query_users = user.AsQueryable();
            
            mock_users.As<IQueryable<ApplicationUser>>().Setup(m => m.Provider).Returns(query_users.Provider);
            mock_users.As<IQueryable<ApplicationUser>>().Setup(m => m.Expression).Returns(query_users.Expression);
            mock_users.As<IQueryable<ApplicationUser>>().Setup(m => m.ElementType).Returns(query_users.ElementType);
            mock_users.As<IQueryable<ApplicationUser>>().Setup(m => m.GetEnumerator()).Returns(() => query_users.GetEnumerator());

            //mock_context.Setup(c => c.TweeterUsers).Returns(/* Tweeter User list?*/);
            /*
             bleow mocks the "users' getter that reutns a slist of appicatiton usres
             
             mock_user_manager_context.Setup(c => c.TweeterUsers).Returns(mock_users.Object);

            */


            //if wer just add a username filed to the twit model
            //mock_context.Setup(c => c.TweeterUsers).Returns(/* Tweeter User list?*/);

            //tricking LINQ to to think list is a database table
            mock_tweet_table.As<IQueryable<Tweet>>().Setup(m => m.Provider).Returns(queryable_list.Provider);
            mock_tweet_table.As<IQueryable<Tweet>>().Setup(m => m.Expression).Returns(queryable_list.Expression);
            mock_tweet_table.As<IQueryable<Tweet>>().Setup(m => m.ElementType).Returns(queryable_list.ElementType);
            mock_tweet_table.As<IQueryable<Tweet>>().Setup(m => m.GetEnumerator()).Returns(() => queryable_list.GetEnumerator());

            //response property returns fake database table
            mock_context.Setup(c => c.Tweets).Returns(mock_tweet_table.Object);

            //define callback for response to a called method
            mock_tweet_table.Setup(t => t.Add(It.IsAny<Tweet>())).Callback((Tweet s) => tweeter_list.Add(s));

        }


        [TestInitialize] //runs before any tests
        public void Intialize()
        {
            //create
            mock_users = new Mock<DbSet<ApplicationUser>>();
            mock_context = new Mock<TweeterContext>();
            mock_tweet_table = new Mock<DbSet<Tweet>>();
            tweeter_list = new List<Tweet>();  //fake database
            repo = new TweeterRepository(mock_context.Object);

            ConnectMocksToDataStore();

        }

        [TestCleanup] //runs after every test
        public void TearDown()
        {
            repo = null; //reset repo 
        }



        public void RepoEnsureCanCreateInstance()
        {
            TweeterRepository repo = new TweeterRepository();
            Assert.IsNotNull(repo);
        }

        [TestMethod]
        public void RepoEnsureRepoHasContext()
        {
            TweeterRepository repo = new TweeterRepository();
            TweeterContext actual_context = repo.Context;

            Assert.IsInstanceOfType(actual_context, typeof(TweeterContext));
        }

        [TestMethod]
        public void RepoEnsureIcanGetUserNames()
        {
            // Arrange
           
     

            // Act
           

            // Assert
           
        }
















    }
}
