using LAAuto.Services;
using LAAuto.Services.Categories;
using LAAuto.Services.Impl.Appointments;
using LAAuto.Services.Users;
using LAAuto.Web.Controllers;
using LAAuto.Web.Models.Categories;
using LAAuto.Web.Models.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Routing;
using Moq;
using System.Security.Claims;
using SERVICES = LAAuto.Services.Services;

namespace LAAuto.Tests.Controllers
{
    public class ServiceControllerTests
    {
        private readonly Claim nameIdentifier = new(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString());
        private readonly MockRepository _mockRepository;
        private readonly Mock<IWebHostEnvironment> hostEnvironmentMock;
        private readonly Mock<SERVICES.IServiceService> serviceServiceMock;
        private readonly Mock<ICategoryService> categoryServiceMock;
        private readonly Mock<IUserService> userServiceMock;
        private readonly Mock<HttpContext> httpContextMock;


        public ServiceControllerTests()
        {
            _mockRepository = new MockRepository(MockBehavior.Loose);
            hostEnvironmentMock = _mockRepository.Create<IWebHostEnvironment>();
            serviceServiceMock = _mockRepository.Create<SERVICES.IServiceService>();
            categoryServiceMock = _mockRepository.Create<ICategoryService>();
            userServiceMock = _mockRepository.Create<IUserService>();

            httpContextMock = new Mock<HttpContext>();
            httpContextMock.Setup(m => m.User.FindFirst(ClaimTypes.NameIdentifier)).Returns(nameIdentifier);
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
            var categories = new List<Category>();

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
        public async Task CreateService_RedirectsToListServices()
        {
            #region Arrange

            var request = new CreateServiceRequest()
            {
                UserId = Guid.NewGuid(),
                OpenTime = TimeOnly.MinValue.ToString(),
                CloseTime = TimeOnly.MaxValue.ToString(),
                Description = "Description",
                Name = "Name",
                Location = "TestLocation",
                Categories = new List<SelectCategoryViewModel>()
                {
                    new SelectCategoryViewModel()
                    {
                        IsSelected= true,
                    }
                },
            };

            var categories = new List<Category>();

            categoryServiceMock.Setup(x => x.ListCategoriesAsync(
                    It.IsAny<Guid?>()))
                .Returns(Task.FromResult(categories));

            #endregion

            #region Act
            var context = new ControllerContext(new ActionContext(httpContextMock.Object, new RouteData(), new ControllerActionDescriptor()));

            var controller = new ServiceController
               (serviceServiceMock.Object, userServiceMock.Object, categoryServiceMock.Object, hostEnvironmentMock.Object)
            {
                ControllerContext = context
            };

            var result = controller.Create(request);

            #endregion

            #region Assert

            Assert.NotNull(result);

            var expected = await result as RedirectToActionResult;

            Assert.NotNull(expected);

            Assert.Equal("Mine", expected.ActionName);

            #endregion
        }

        [Fact]
        public async Task CreateService_ReturnsViewWithModelIfThereIsNoSelectedCategory()
        {
            #region Arrange

            var request = new CreateServiceRequest()
            {
                UserId = Guid.NewGuid(),
                OpenTime = TimeOnly.MinValue.ToString(),
                CloseTime = TimeOnly.MaxValue.ToString(),
                Description = "Description",
                Name = "Name",
                Location = "TestLocation",
                Categories = new List<SelectCategoryViewModel>()
                {
                    new SelectCategoryViewModel()
                    {
                        IsSelected= false,
                    }
                },
            };

            var categories = new List<Category>();

            categoryServiceMock.Setup(x => x.ListCategoriesAsync(
                    It.IsAny<Guid?>()))
                .Returns(Task.FromResult(categories));

            #endregion

            #region Act
            var context = new ControllerContext(new ActionContext(httpContextMock.Object, new RouteData(), new ControllerActionDescriptor()));

            var controller = new ServiceController
               (serviceServiceMock.Object, userServiceMock.Object, categoryServiceMock.Object, hostEnvironmentMock.Object)
            {
                ControllerContext = context
            };

            var result = controller.Create(request);

            #endregion

            #region Assert

            Assert.NotNull(result);

            var expected = await result as ViewResult;

            Assert.NotNull(expected);

            Assert.IsType<CreateServiceRequest>(expected.Model);

            #endregion
        }
        [Fact]
        public async Task CreateService_ReturnsViewWithModelIfOpenTimeOrCloseTimeIsIncorrect()
        {
            #region Arrange

            var request = new CreateServiceRequest()
            {
                UserId = Guid.NewGuid(),
                OpenTime = TimeOnly.MaxValue.ToString(),
                CloseTime = TimeOnly.MinValue.ToString(),
                Description = "Description",
                Name = "Name",
                Location = "TestLocation",
                Categories = new List<SelectCategoryViewModel>()
                {
                    new SelectCategoryViewModel()
                    {
                        IsSelected= true,
                    }
                },
            };

            var categories = new List<Category>();

            categoryServiceMock.Setup(x => x.ListCategoriesAsync(
                    It.IsAny<Guid?>()))
                .Returns(Task.FromResult(categories));

            #endregion

            #region Act
            var context = new ControllerContext(new ActionContext(httpContextMock.Object, new RouteData(), new ControllerActionDescriptor()));

            var controller = new ServiceController
               (serviceServiceMock.Object, userServiceMock.Object, categoryServiceMock.Object, hostEnvironmentMock.Object)
            {
                ControllerContext = context
            };

            var result = controller.Create(request);

            #endregion

            #region Assert

            Assert.NotNull(result);

            var expected = await result as ViewResult;

            Assert.NotNull(expected);

            Assert.IsType<CreateServiceRequest>(expected.Model);

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
        public async Task UpdateRating_ThrowslIfParameterIsNull()
        {
            #region Arrange

            #endregion

            #region Act

            var controller = new ServiceController
             (serviceServiceMock.Object, userServiceMock.Object, categoryServiceMock.Object, hostEnvironmentMock.Object);

            #endregion

            #region Assert

            await Assert.ThrowsAsync<ArgumentNullException>(async ()
               => await controller.UpdateRating(null));

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

        [Fact]
        public async Task UpdateServiceRating_RedirectsToGetService()
        {
            #region Arrange

            var request = new UpdateRatingRequest();

            #endregion

            #region Act
            var context = new ControllerContext(new ActionContext(httpContextMock.Object, new RouteData(), new ControllerActionDescriptor()));

            var controller = new ServiceController
               (serviceServiceMock.Object, userServiceMock.Object, categoryServiceMock.Object, hostEnvironmentMock.Object)
            {
                ControllerContext = context
            };

            var result = controller.UpdateRating(request);

            #endregion

            #region Assert

            Assert.NotNull(result);

            var expected = await result as RedirectToActionResult;

            Assert.NotNull(expected);

            Assert.Equal("Get", expected.ActionName);

            #endregion
        }

    }
}
