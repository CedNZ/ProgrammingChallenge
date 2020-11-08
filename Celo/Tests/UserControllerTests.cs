using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Moq;
using Celo;
using Celo.Repository;
using Celo.Controllers;
using Celo.Model;
using FluentAssertions;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Tests
{
    public class UserControllerTests
    {
        private User alice = new User
        {
            Id = 1,
            FirstName = "Alice",
            LastName = "One",
            Title = "Mrs",
        };

        private User bob = new User
        {
            Id = 2,
            FirstName = "Bob",
            LastName = "Two",
            Title = "Mr",
        };

        private User charles = new User
        {
            Id = 3,
            FirstName = "Charles",
            LastName = "Three",
            Title = "Dr",
        };

        [Fact]
        public void GetUsers_AsJson_ReturnsAllUsers()
        {
            var userRepositoryMock = new Mock<IUserRepository>();

            userRepositoryMock.Setup(x => x.GetUsers()).Returns(new List<User> { alice, bob, charles }).Verifiable();

            var userController = new UserController(userRepositoryMock.Object);

            var result = userController.Index(asJson: true);

            result.Should().NotBeNull();

            result.Should().BeOfType(typeof(JsonResult));

            IEnumerable<User> users = ((JsonResult)result).Value as IEnumerable<User>;

            users.Should().HaveCount(3);
            users.Last().Name.Should().Be(charles.Name);
            Mock.VerifyAll();
        }

        [Fact]
        public void GetUsers_MaxRecords_ReturnsSubset()
        {
            var userRepositoryMock = new Mock<IUserRepository>();

            userRepositoryMock.Setup(x => x.GetUsers()).Returns(new List<User> { alice, bob, charles }).Verifiable();

            var userController = new UserController(userRepositoryMock.Object);

            var result = userController.Index(maxRecords: 1, asJson: true);

            result.Should().NotBeNull();

            result.Should().BeOfType(typeof(JsonResult));

            IEnumerable<User> users = ((JsonResult)result).Value as IEnumerable<User>;

            users.Should().HaveCount(1);
            users.Last().Name.Should().Be(alice.Name);
            Mock.VerifyAll();
        }

        [Fact]
        public void GetUsers_NameSearch_MatchesFirstName()
        {
            var userRepositoryMock = new Mock<IUserRepository>();

            userRepositoryMock.Setup(x => x.GetUsers()).Returns(new List<User> { alice, bob, charles }).Verifiable();

            var userController = new UserController(userRepositoryMock.Object);

            var result = userController.Index(nameSearch: "bob", asJson: true);

            result.Should().NotBeNull();

            result.Should().BeOfType(typeof(JsonResult));

            IEnumerable<User> users = ((JsonResult)result).Value as IEnumerable<User>;

            users.Should().HaveCount(1);
            users.Last().Name.Should().Be(bob.Name);
            Mock.VerifyAll();
        }

        [Fact]
        public void GetUsers_NameSearch_MatchesLastName()
        {
            var userRepositoryMock = new Mock<IUserRepository>();

            userRepositoryMock.Setup(x => x.GetUsers()).Returns(new List<User> { alice, bob, charles }).Verifiable();

            var userController = new UserController(userRepositoryMock.Object);

            var result = userController.Index(nameSearch: "one", asJson: true);

            result.Should().NotBeNull();

            result.Should().BeOfType(typeof(JsonResult));

            IEnumerable<User> users = ((JsonResult)result).Value as IEnumerable<User>;

            users.Should().HaveCount(1);
            users.Last().Name.Should().Be(alice.Name);
            Mock.VerifyAll();
        }

        [Fact]
        public void GetUsers_NameSearch_MatchesTitle()
        {
            var userRepositoryMock = new Mock<IUserRepository>();

            userRepositoryMock.Setup(x => x.GetUsers()).Returns(new List<User> { alice, bob, charles }).Verifiable();

            var userController = new UserController(userRepositoryMock.Object);

            var result = userController.Index(nameSearch: "dr", asJson: true);

            result.Should().NotBeNull();

            result.Should().BeOfType(typeof(JsonResult));

            IEnumerable<User> users = ((JsonResult)result).Value as IEnumerable<User>;

            users.Should().HaveCount(1);
            users.Last().Name.Should().Be(charles.Name);
            Mock.VerifyAll();
        }

        [Fact]
        public void GetUsers_NoParams_ReturnsWebpage()
        {
            var userRepositoryMock = new Mock<IUserRepository>();

            userRepositoryMock.Setup(x => x.GetUsers()).Returns(new List<User> { alice, bob, charles }).Verifiable();

            var userController = new UserController(userRepositoryMock.Object);

            var result = userController.Index();

            result.Should().NotBeNull();

            result.Should().BeOfType(typeof(ViewResult));

            (((ViewResult)result).Model as IEnumerable<User>).Should().HaveCount(3);

            Mock.VerifyAll();
        }

        [Fact]
        public void GetUser_ById_Returns200()
        {
            var userRepositoryMock = new Mock<IUserRepository>();

            userRepositoryMock.Setup(x => x.GetUserById(It.IsAny<int>())).Returns(alice).Verifiable();

            var userController = new UserController(userRepositoryMock.Object);

            var result = userController.Get(1);

            result.Should().NotBeNull();

            result.Should().BeOfType<OkObjectResult>()
                .Which.StatusCode.Should().Be(200);
            Mock.VerifyAll();
        }

        [Fact]
        public void GetUser_ById_Returns500()
        {
            var userRepositoryMock = new Mock<IUserRepository>();

            var userController = new UserController(userRepositoryMock.Object);

            var result = userController.Get(1);

            result.Should().NotBeNull();

            result.Should().BeOfType<ObjectResult>()
                .Which.StatusCode.Should().Be(500);

            userRepositoryMock.Verify(x => x.GetUserById(It.IsAny<int>()));
        }

        [Fact]
        public void UpdateUser_Succeeds()
        {
            var userRepositoryMock = new Mock<IUserRepository>();

            userRepositoryMock.Setup(x => x.UpdateUser(It.IsAny<int>(), It.IsAny<User>())).Returns(true);

            var userController = new UserController(userRepositoryMock.Object);

            var result = userController.Update(alice.Id, alice);

            result.Should().NotBeNull();

            result.Should().BeOfType<RedirectToActionResult>();

            userRepositoryMock.Verify(x => x.UpdateUser(It.Is<int>(i => i == 1), It.Is<User>(u => u.Name == alice.Name)));
        }

        [Fact]
        public void UpdateUser_Returns500()
        {
            var userRepositoryMock = new Mock<IUserRepository>();

            userRepositoryMock.Setup(x => x.UpdateUser(It.IsAny<int>(), It.IsAny<User>())).Returns(false);

            var userController = new UserController(userRepositoryMock.Object);

            var result = userController.Update(alice.Id, alice);

            result.Should().NotBeNull();

            result.Should().BeOfType<ObjectResult>()
                .Which.StatusCode.Should().Be(500);

            userRepositoryMock.Verify(x => x.UpdateUser(It.Is<int>(i => i == 1), It.Is<User>(u => u.Name == alice.Name)));
        }

        [Fact]
        public void DeleteUser_Returns200()
        {
            var userRepositoryMock = new Mock<IUserRepository>();

            userRepositoryMock.Setup(x => x.DeleteUser(It.IsAny<int>())).Returns(true).Verifiable();

            var userController = new UserController(userRepositoryMock.Object);

            var result = userController.Delete(alice.Id);

            result.Should().NotBeNull();

            result.Should().BeOfType<OkObjectResult>()
                .Which.StatusCode.Should().Be(200);

            Mock.VerifyAll();
        }

        [Fact]
        public void DeleteUser_Returns500()
        {
            var userRepositoryMock = new Mock<IUserRepository>();

            userRepositoryMock.Setup(x => x.DeleteUser(It.IsAny<int>())).Returns(false).Verifiable();

            var userController = new UserController(userRepositoryMock.Object);

            var result = userController.Delete(alice.Id);

            result.Should().NotBeNull();

            result.Should().BeOfType<ObjectResult>()
                .Which.StatusCode.Should().Be(500);

            Mock.VerifyAll();
        }
    }
}
