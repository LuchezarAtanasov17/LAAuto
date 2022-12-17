using LAAuto.Services.Categories;
using LAAuto.Services.Services;
using LAAuto.Services.Users;
using LAAuto.Web.Controllers;
using LAAuto.Web.Models.Appointments;
using LAAuto.Web.Models.Categories;
using LAAuto.Web.Models.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Routing;
using Moq;
using System;
using System.Net.Http;
using System.Security.Claims;
using System.Xml.Linq;
using SERVICE_APPOINTMENTS = LAAuto.Services.Appointments;

namespace LAAuto.Tests.Controllers
{
    public class AppointmentControllerTests
    {
        private readonly Claim nameIdentifier = new(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString());

        private readonly MockRepository _mockRepository;
        private readonly Mock<SERVICE_APPOINTMENTS.IAppointmentService> appointmentServiceMock;
        private readonly Mock<IServiceService> serviceServiceMock;
        private readonly Mock<ICategoryService> categoryServiceMock;
        private readonly Mock<HttpContext> httpContextMock;

        public AppointmentControllerTests()
        {
            _mockRepository = new MockRepository(MockBehavior.Loose);
            appointmentServiceMock = _mockRepository.Create<SERVICE_APPOINTMENTS.IAppointmentService>();
            serviceServiceMock = _mockRepository.Create<IServiceService>();
            categoryServiceMock = _mockRepository.Create<ICategoryService>();

            httpContextMock = new Mock<HttpContext>();
            httpContextMock.Setup(m => m.User.FindFirst(ClaimTypes.NameIdentifier)).Returns(nameIdentifier);
        }

        [Fact]
        public async Task ListAppointments_ReturnsViewWithAppointments()
        {
            #region Arrange

            var serviceAppointments = new List<SERVICE_APPOINTMENTS.Appointment>()
            {
                new SERVICE_APPOINTMENTS.Appointment
                {
                    Id = Guid.NewGuid(),
                    CategoryId= Guid.NewGuid(),
                    Description= "Test2",
                    EndDate= DateTime.Now,
                    StartDate= DateTime.Now,
                    ServiceId   = Guid.NewGuid(),
                    UserId= Guid.NewGuid(),
                    User = new User(),
                    Service = new Service(),
                },
                new SERVICE_APPOINTMENTS.Appointment
                {
                    Id = Guid.NewGuid(),
                    CategoryId= Guid.NewGuid(),
                    Description= "Test",
                    EndDate= DateTime.Now,
                    StartDate= DateTime.Now,
                    ServiceId   = Guid.NewGuid(),
                    UserId= Guid.NewGuid(),
                    User = new User(),
                    Service = new Service(),
                }
            };

            appointmentServiceMock.Setup(x => x.ListAppointmentsAsync(
                    It.IsAny<Guid?>(), It.IsAny<Guid?>()))
                .Returns(Task.FromResult(serviceAppointments));

            #endregion

            #region Act

            var controller = new AppointmentController
                (appointmentServiceMock.Object, serviceServiceMock.Object, categoryServiceMock.Object);

            var result = await controller.List();

            #endregion

            #region Assert

            Assert.NotNull(result);

            var expected = result as ViewResult;

            Assert.NotNull(expected);

            Assert.IsType<List<AppointmentViewModel>>(expected.Model);

            var controllerAppointments = expected.Model as List<AppointmentViewModel>;

            List<Guid> controllerItemIds = controllerAppointments!.Select(x => x.Id).ToList();
            List<Guid> serviceItemIds = serviceAppointments!.Select(x => x.Id).ToList();

            List<Guid> missingIds = serviceItemIds.Except(controllerItemIds).ToList();
            List<Guid> unexpectedIds = controllerItemIds.Except(serviceItemIds).ToList();

            Assert.Empty(missingIds);
            Assert.Empty(unexpectedIds);

            #endregion
        }

        [Fact]
        public async Task GetCreateAppointment_ReturnViewWithAppointment()
        {
            #region Arrange
            var service = new Service()
            {
                User = new User(),
                Categories = new List<Category>()
            };


            var appointmentRequest = new CreateAppointmentRequest()
            {
                ServiceId = Guid.NewGuid(),
                Service = new ServiceViewModel()
                {
                    User = new Web.Models.Users.UserViewModel(),
                    Categories = new List<CategoryViewModel>()
                }
            };

            serviceServiceMock.Setup(x => x.GetServiceAsync(
                    appointmentRequest.ServiceId))
                .Returns(Task.FromResult(service));

            #endregion

            #region Act

            var controller = new AppointmentController
                (appointmentServiceMock.Object, serviceServiceMock.Object, categoryServiceMock.Object);
            var result = controller.Create(appointmentRequest.ServiceId);

            #endregion

            #region Assert

            Assert.NotNull(result);

            var expected = await result as ViewResult;

            Assert.NotNull(expected);
            Assert.IsType<CreateAppointmentRequest>(expected.Model);

            var model = expected.Model as CreateAppointmentRequest;

            Assert.NotNull(model);
            Assert.Equal(appointmentRequest.ServiceId, model.ServiceId);
            #endregion
        }

        [Fact]
        public async Task CreateAppointment_RedirectsToListAppointments()
        {
            #region Arrange
            var service = new Service()
            {
                User = new User(),
                Categories = new List<Category>()
                {
                    new Category()
                    {
                        Id = Guid.NewGuid(),
                        Name = "Test"
                    },
                    new Category()
                    {
                        Id = Guid.NewGuid(),
                        Name = "Test2"
                    }
                }
            };

            serviceServiceMock.Setup(x => x.GetServiceAsync(
                     It.IsAny<Guid>()))
                .Returns(Task.FromResult(service));

            var category = new Category();

            categoryServiceMock.Setup(x => x.GetCategoryAsync(
                     It.IsAny<Guid>()))
                .Returns(Task.FromResult(category));

            var createRequest = new CreateAppointmentRequest()
            {
                ServiceId = Guid.NewGuid(),
                Service = new ServiceViewModel()
                {
                    User = new Web.Models.Users.UserViewModel(),
                    Categories = new List<CategoryViewModel>()
                },
                CategoryId = Guid.NewGuid(),
                StartDate = DateTime.Now.AddHours(1),
            };

            appointmentServiceMock.Setup(x => x.CreateAppointmentAsync(
                     It.IsAny<SERVICE_APPOINTMENTS.CreateAppointmentRequest>()))
                .Returns(Task.FromResult(createRequest));

            #endregion

            #region Act

            var context = new ControllerContext(new ActionContext(httpContextMock.Object, new RouteData(), new ControllerActionDescriptor()));
            var controller = new AppointmentController(appointmentServiceMock.Object, serviceServiceMock.Object, categoryServiceMock.Object)
            {
                ControllerContext = context
            };

            var result = await controller.Create(createRequest.ServiceId, createRequest);

            #endregion

            #region Assert

            Assert.NotNull(result);

            var expected = result as RedirectToActionResult;
            Assert.NotNull(expected);
            Assert.Equal("List", expected.ActionName);



            #endregion
        }

        [Fact]
        public async Task GetUpdateAppointment_ReturnsViewWithAppointment()
        {
            #region Arrange

            var serviceAppointment = new SERVICE_APPOINTMENTS.Appointment
            {
                Id = Guid.NewGuid(),
                CategoryId = Guid.NewGuid(),
                Description = "Test2",
                EndDate = DateTime.Now,
                StartDate = DateTime.Now,
                ServiceId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                User = new User(),
                Service = new Service()
                {
                    User = new User(),
                    Categories = new List<Category>()
                    {
                        new Category()
                        {
                            Id = Guid.NewGuid(),
                            Name = "Test"
                        },
                        new Category()
                        {
                            Id = Guid.NewGuid(),
                            Name = "Test2"
                        }
                    }
                },
            };

            appointmentServiceMock.Setup(x => x.GetAppointmentAsync(
                    It.IsAny<Guid>()))
                .Returns(Task.FromResult(serviceAppointment));

            serviceServiceMock.Setup(x => x.GetServiceAsync(
                    It.IsAny<Guid>()))
                .Returns(Task.FromResult(serviceAppointment.Service));

            #endregion

            #region Act

            var controller = new AppointmentController
                (appointmentServiceMock.Object, serviceServiceMock.Object, categoryServiceMock.Object);

            var result = await controller.Update(serviceAppointment.Id);

            #endregion

            #region Assert

            Assert.NotNull(result);

            var expected = result as ViewResult;

            Assert.NotNull(expected);

            Assert.IsType<UpdateAppointmentRequest>(expected.Model);

            var controllerAppointment = expected.Model as UpdateAppointmentRequest;

            Assert.Equal(controllerAppointment.Id, serviceAppointment.Id);
            #endregion
        }

        [Fact]
        public async Task UpdateAppointment_RedirectsToListAppointment()
        {
            #region Arrange

            var appointment = new UpdateAppointmentRequest
            {
                Id = Guid.NewGuid(),
                CategoryId = Guid.NewGuid(),
                Description = "Test2",
                StartDate = DateTime.Now.AddHours(1),
                EndDate = DateTime.Now.AddHours(1),
                ServiceId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                Service = new ServiceViewModel(),
            };

            appointmentServiceMock.Setup(x => x.UpdateAppointmentAsync(
                    It.IsAny<Guid>(), It.IsAny<SERVICE_APPOINTMENTS.UpdateAppointmentRequest>()))
                .Returns(Task.FromResult(appointment));

            var serviceService = new Service()
            {
                User = new User(),
                Categories = new List<Category>()
            };
            serviceServiceMock.Setup(x => x.GetServiceAsync(
                   It.IsAny<Guid>()))
               .Returns(Task.FromResult(serviceService));

            #endregion

            #region Act

            var context = new ControllerContext(new ActionContext(httpContextMock.Object, new RouteData(), new ControllerActionDescriptor()));
            var controller = new AppointmentController
                (appointmentServiceMock.Object, serviceServiceMock.Object, categoryServiceMock.Object)
            {
                ControllerContext = context
            };

            var result = await controller.Update(appointment.Id, appointment);

            #endregion

            #region Assert

            Assert.NotNull(result);

            var expected = result as RedirectToActionResult;

            Assert.NotNull(expected);
            Assert.Equal("List", expected.ActionName);

            #endregion
        }

        [Fact]
        public async Task DeleteAppointment_RedirectsToListCategories()
        {
            #region Arrange

            appointmentServiceMock.Setup(x => x.DeleteAppointmentAsync(
                    It.IsAny<Guid>()))
                .Returns(Task.CompletedTask);

            #endregion

            #region Act

            var controller = new AppointmentController(appointmentServiceMock.Object, serviceServiceMock.Object, categoryServiceMock.Object);

            var result = await controller.Delete(Guid.NewGuid());

            #endregion

            #region Assert

            Assert.NotNull(result);

            var expected = result as RedirectToActionResult;
            Assert.NotNull(expected);
            Assert.Equal("List", expected.ActionName);

            #endregion
        }

        [Fact]
        public async Task DeleteMyAppointment_RedirectsToListCategories()
        {
            #region Arrange

            appointmentServiceMock.Setup(x => x.DeleteAppointmentAsync(
                    It.IsAny<Guid>()))
                .Returns(Task.CompletedTask);

            #endregion

            #region Act

            var context = new ControllerContext(new ActionContext(httpContextMock.Object, new RouteData(), new ControllerActionDescriptor()));
            var controller = new AppointmentController(appointmentServiceMock.Object, serviceServiceMock.Object, categoryServiceMock.Object)
            {
                ControllerContext = context
            };

            var result = await controller.DeleteMyAppointment(Guid.NewGuid());

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
