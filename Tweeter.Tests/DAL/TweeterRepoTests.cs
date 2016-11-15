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
        private Mock<TweeterContext> mock_context { get; set; }
        private TweeterRepository Repo { get; set; }
        private List<Twit> users { get; set; }

        [TestInitialize]
        public void Initialize()
        {
            mock_context = new Mock<TweeterContext>();
            mock_users = new Mock<DbSet<Twit>>();
            Repo = new TweeterRepository(mock_context.Object);
            users = new List<Twit>
            {
<<<<<<< Updated upstream
                new Twit {
                    TwitId = 1,
                    BaseUser = new ApplicationUser() { UserName = "michealb"}
                },
                new Twit {
                    TwitId = 2,
                    BaseUser = new ApplicationUser() { UserName = "sallym"}
                }

            };
=======
                new Twit
                {
                    TwitId = 1,
                    BaseUser = new ApplicationUser() { UserName= "michael" }
            },
                new Twit
                {
                    TwitId = 2,
                    BaseUser = new ApplicationUser() { UserName="sally" }          
                }
            };
        }
            
>>>>>>> Stashed changes

            /* 
             1. Install Identity into Tweeter.Tests (using statement needed)
             2. Create a mock context that uses 'UserManager' instead of 'TweeterContext'
             */


        public void ConnectToDatastore()
        {
<<<<<<< Updated upstream
=======
            users = new List<Twit>();
>>>>>>> Stashed changes
            var query_users = users.AsQueryable();

            mock_users.As<IQueryable<Twit>>().Setup(m => m.Provider).Returns(query_users.Provider);
            mock_users.As<IQueryable<Twit>>().Setup(m => m.Expression).Returns(query_users.Expression);
            mock_users.As<IQueryable<Twit>>().Setup(m => m.ElementType).Returns(query_users.ElementType);
            mock_users.As<IQueryable<Twit>>().Setup(m => m.GetEnumerator()).Returns(() => query_users.GetEnumerator());
<<<<<<< Updated upstream
=======

            mock_context.Setup(c => c.TweeterUsers).Returns(mock_users.Object);

            mock_users.Setup(u => u.Add(It.IsAny<Twit>())).Callback((Twit t) => users.Add(t));
>>>>>>> Stashed changes

            mock_context.Setup(c => c.TweeterUsers).Returns(mock_users.Object);
            mock_users.Setup(u => u.Add(It.IsAny<Twit>())).Callback((Twit t) => users.Add(t));
            /*
             * Below mocks the 'Users' getter that returns a list of ApplicationUsers
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
<<<<<<< Updated upstream
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
=======
            ConnectToDatastore();
            List<string> usernames = Repo.GetUserNames();
            Assert.AreEqual(2, usernames.Count);
        }

        [TestMethod]
        public void RepoEnsureUsernameExists()
        {
            ConnectToDatastore();
            bool exists = Repo.UsernameExists("sally");
            Assert.IsTrue(exists);
>>>>>>> Stashed changes
        }
    }
}
