using LAAuto.Services.Categories;
using LAAuto.Services.Users;
using LAAuto.Web.Controllers;
using LAAuto.Web.Models.Categories;
using LAAuto.Web.Models.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SERVICES = LAAuto.Services.Services;

namespace LAAuto.Tests.Controllers
{
    public class ServiceControllerTests
    {
        private readonly MockRepository _mockRepository;
        private readonly Mock<IWebHostEnvironment> hostEnvironmentMock;
        private readonly Mock<SERVICES.IServiceService> serviceServiceMock;
        private readonly Mock<ICategoryService> categoryServiceMock;
        private readonly Mock<IUserService> userServiceMock;

        public ServiceControllerTests()
        {
            _mockRepository = new MockRepository(MockBehavior.Loose);
            hostEnvironmentMock = _mockRepository.Create<IWebHostEnvironment>();
            serviceServiceMock = _mockRepository.Create<SERVICES.IServiceService>();
            categoryServiceMock = _mockRepository.Create<ICategoryService>();
            userServiceMock = _mockRepository.Create<IUserService>();
        }

        [Fact]
        public async Task GetService_ReturnsViewWithService()
        {
            #region Arrange

            var serviceService = new SERVICES.Service
            {
                Id = Guid.NewGuid(),
                User = new User(),
                Categories = new List<Category>()
            };

            serviceServiceMock.Setup(x => x.GetServiceAsync(
                    It.IsAny<Guid>()))
                .Returns(Task.FromResult(serviceService));

            #endregion

            #region Act

            var controller = new ServiceController
                (serviceServiceMock.Object, userServiceMock.Object, categoryServiceMock.Object, hostEnvironmentMock.Object);

            var result = await controller.Get(serviceService.Id);

            #endregion

            #region Assert

            Assert.NotNull(result);

            var expected = result as ViewResult;

            Assert.NotNull(expected);

            Assert.IsType<ServiceViewModel>(expected.Model);

            var controllerService = expected.Model as ServiceViewModel;

            Assert.Equal(controllerService.Id, serviceService.Id);
            #endregion
        }

        [Fact]
        public async Task GetCreateService_ReturnViewWithService()
        {
            #region Arrange
            var categories = new List<Category>()
            {

            };

            categoryServiceMock.Setup(x => x.ListCategoriesAsync(
                    It.IsAny<Guid?>()))
                .Returns(Task.FromResult(categories));

            #endregion

            #region Act

            var controller = new ServiceController
               (serviceServiceMock.Object, userServiceMock.Object, categoryServiceMock.Object, hostEnvironmentMock.Object);

            var result = controller.Create();

            #endregion

            #region Assert

            Assert.NotNull(result);

            var expected = await result as ViewResult;

            Assert.NotNull(expected);
            Assert.IsType<CreateServiceRequest>(expected.Model);

            var model = expected.Model as CreateServiceRequest;

            Assert.NotNull(model);
            #endregion
        }

        [Fact]
        public async Task GetUpdateService_ReturnsViewWithService()
        {
            #region Arrange

            var serviceService = new UpdateServiceRequest()
            {
                Id = Guid.NewGuid(),
            };

            var service = new SERVICES.Service()
            {
                Id = serviceService.Id,
            };

            var categories = new List<Category>();


            serviceServiceMock.Setup(x => x.GetServiceAsync(
                    It.IsAny<Guid>()))
                .Returns(Task.FromResult(service));

            categoryServiceMock.Setup(x => x.ListCategoriesAsync(
                    It.IsAny<Guid?>()))
                .Returns(Task.FromResult(categories));

            #endregion

            #region Act

            var controller = new ServiceController
              (serviceServiceMock.Object, userServiceMock.Object, categoryServiceMock.Object, hostEnvironmentMock.Object);

            var result = await controller.Update(serviceService.Id);

            #endregion

            #region Assert

            Assert.NotNull(result);

            var expected = result as ViewResult;

            Assert.NotNull(expected);

            Assert.IsType<UpdateServiceRequest>(expected.Model);

            var controllerService = expected.Model as UpdateServiceRequest;

            Assert.Equal(controllerService.Id, serviceService.Id);
            #endregion
        }

        [Fact]
        public async Task UpdateService_ReturnViewWithCorrectModelIfModelStateIsNotValid()
        {
            #region Arrange

            var service = new UpdateServiceRequest
            {
                Id = Guid.NewGuid(),
                Categories = new List<SelectCategoryViewModel>()
            };

            serviceServiceMock.Setup(x => x.UpdateServiceAsync(
                    It.IsAny<Guid>(), It.IsAny<SERVICES.UpdateServiceRequest>()))
                .Returns(Task.FromResult(service));

            #endregion

            #region Act

            var controller = new ServiceController
             (serviceServiceMock.Object, userServiceMock.Object, categoryServiceMock.Object, hostEnvironmentMock.Object);

            var result = await controller.Update(service.Id, service);

            #endregion

            #region Assert

            Assert.NotNull(result);

            var expected = result as ViewResult;

            Assert.NotNull(expected);
            
            var model = expected.Model;
            Assert.NotNull(model);
            Assert.IsType<UpdateServiceRequest>(model);
            #endregion
        }

        [Fact]
        public async Task UpdateService_RedirectsToGetService()
        {
            #region Arrange

            var service = new UpdateServiceRequest
            {
                Id = Guid.NewGuid(),
                OpenTime = new TimeOnly(2, 30).ToLongTimeString(),
                CloseTime = new TimeOnly(8,30).ToLongTimeString(),
                Location = "TestLocation1",
                Name = "Name",
                Categories = new List<SelectCategoryViewModel>()
                {
                    new SelectCategoryViewModel{ Id = Guid.NewGuid(), IsSelected = true }
                }
            };

            serviceServiceMock.Setup(x => x.UpdateServiceAsync(
                    It.IsAny<Guid>(), It.IsAny<SERVICES.UpdateServiceRequest>()))
                .Returns(Task.FromResult(service));

            #endregion

            #region Act

            var controller = new ServiceController
             (serviceServiceMock.Object, userServiceMock.Object, categoryServiceMock.Object, hostEnvironmentMock.Object);

            var result = await controller.Update(service.Id, service);

            #endregion

            #region Assert

            Assert.NotNull(result);

            var expected = result as RedirectToActionResult;

            Assert.NotNull(expected);
            Assert.Equal("Get", expected.ActionName);
            #endregion
        }

        [Fact]
        public async Task DeleteService_RedirectsToListServices()
        {
            #region Arrange

            serviceServiceMock.Setup(x => x.DeleteServiceAsync(
                    It.IsAny<Guid>()))
                .Returns(Task.CompletedTask);

            #endregion

            #region Act

            var controller = new ServiceController
                        (serviceServiceMock.Object, userServiceMock.Object, categoryServiceMock.Object, hostEnvironmentMock.Object);

            var result = await controller.Delete(Guid.NewGuid());

            #endregion

            #region Assert

            Assert.NotNull(result);

            var expected = result as RedirectToActionResult;
            Assert.NotNull(expected);
            Assert.Equal("Mine", expected.ActionName);

            #endregion
        }
    }
}
