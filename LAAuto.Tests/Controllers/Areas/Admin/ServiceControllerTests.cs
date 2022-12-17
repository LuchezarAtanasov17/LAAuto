using LAAuto.Services.Categories;
using LAAuto.Services.Services;
using LAAuto.Services.Users;
using LAAuto.Web.Areas.Admin.Controllers;
using LAAuto.Web.Models.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.ComponentModel;

namespace LAAuto.Tests.Controllers.Areas.Admin
{
    [Category("Services")]
    public class ServiceControllerTests
    {
        private readonly MockRepository _mockRepository;

        public ServiceControllerTests()
        {
            _mockRepository = new MockRepository(MockBehavior.Loose);
        }

        [Fact]
        public async Task ListServices_ReturnsViewWithServices()
        {
            #region Arrange

            Mock<IServiceService> serviceServiceMock
                = _mockRepository.Create<IServiceService>();
            Mock<IWebHostEnvironment> hostEnviroment
                = _mockRepository.Create<IWebHostEnvironment>();

            var serviceServices = new List<Service>()
            {
                new Service
                {
                    Id = Guid.NewGuid(),
                    Description= "Test",
                    OpenTime= TimeOnly.MinValue,
                    CloseTime= TimeOnly.MaxValue,
                    UserId= Guid.NewGuid(),
                    User = new User(),
                    AverageRating= 0,
                    Location = "TestLoc",
                    Name = "TestName",
                    Categories = new List<Category>()
                },
                new Service
                {
                   Id = Guid.NewGuid(),
                    Description= "Test2",
                    OpenTime= TimeOnly.MinValue,
                    CloseTime= TimeOnly.MaxValue,
                    UserId= Guid.NewGuid(),
                    User = new User(),
                    AverageRating= 0,
                    Location = "TestLoc2",
                    Name = "TestName2",
                    Categories = new List<Category>()
                }
            };

            serviceServiceMock.Setup(x => x.ListServicesAsync(
                    It.IsAny<Guid?>()))
                .Returns(Task.FromResult(serviceServices));

            #endregion

            #region Act

            var controller = new ServiceController(serviceServiceMock.Object, hostEnviroment.Object);
            var result = await controller.List();

            #endregion

            #region Assert

            Assert.NotNull(result);

            var expected = result as ViewResult;

            Assert.NotNull(expected);

            Assert.IsType<List<ServiceViewModel>>(expected.Model);

            var controllerServices = expected.Model as List<ServiceViewModel>;

            List<Guid> controllerItemIds = controllerServices!.Select(x => x.Id).ToList();
            List<Guid> serviceItemIds = serviceServices!.Select(x => x.Id).ToList();

            List<Guid> missingIds = serviceItemIds.Except(controllerItemIds).ToList();
            List<Guid> unexpectedIds = controllerItemIds.Except(serviceItemIds).ToList();

            Assert.Empty(missingIds);
            Assert.Empty(unexpectedIds);

            #endregion
        }

        [Fact]
        public async Task DeleteService_RedirectsToListServices()
        {
            #region Arrange

            Mock<IServiceService> serviceServiceMock
                = _mockRepository.Create<IServiceService>();
            Mock<IWebHostEnvironment> hostEnviroment
                = _mockRepository.Create<IWebHostEnvironment>();

            serviceServiceMock.Setup(x => x.DeleteServiceAsync(
                    It.IsAny<Guid>()))
                .Returns(Task.CompletedTask);

            #endregion

            #region Act

            var controller = new ServiceController(serviceServiceMock.Object, hostEnviroment.Object);
            var result = await controller.Delete(Guid.NewGuid());

            #endregion

            #region Assert

            Assert.NotNull(result);

            var expected = result as RedirectToActionResult;
            Assert.NotNull(expected);
            Assert.Equal("List", expected.ActionName);

            #endregion
        }
    }
}
