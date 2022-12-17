using LAAuto.Services.Users;
using LAAuto.Web.Areas.Admin.Controllers;
using LAAuto.Web.Models.Users;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.ComponentModel;

namespace LAAuto.Tests.Controllers.Areas.Admin
{
    [Category("Users")]
    public class UserControllerTests
    {
        private readonly MockRepository _mockRepository;

        public UserControllerTests()
        {
            _mockRepository = new MockRepository(MockBehavior.Loose);
        }

        [Fact]
        public async Task ListAppointments_ReturnsViewWithAppointments()
        {
            #region Arrange

            Mock<IUserService> userServiceMock
                = _mockRepository.Create<IUserService>();

            var serviceUsers = new List<User>()
            {
                new User
                {
                    Id = Guid.NewGuid(),
                },
                new User
                {
                    Id = Guid.NewGuid(),
                }
            };

            userServiceMock.Setup(x => x.ListUsersAsync())
                .Returns(Task.FromResult(serviceUsers));

            #endregion

            #region Act

            var controller = new UserController(userServiceMock.Object);
            var result = await controller.List();

            #endregion

            #region Assert

            Assert.NotNull(result);

            var expected = result as ViewResult;

            Assert.NotNull(expected);

            Assert.IsType<List<UserViewModel>>(expected.Model);

            var controllerUsers = expected.Model as List<UserViewModel>;

            List<Guid> controllerItemIds = controllerUsers!.Select(x => x.Id).ToList();
            List<Guid> serviceItemIds = serviceUsers!.Select(x => x.Id).ToList();

            List<Guid> missingIds = serviceItemIds.Except(controllerItemIds).ToList();
            List<Guid> unexpectedIds = controllerItemIds.Except(serviceItemIds).ToList();

            Assert.Empty(missingIds);
            Assert.Empty(unexpectedIds);

            #endregion
        }
    }
}
