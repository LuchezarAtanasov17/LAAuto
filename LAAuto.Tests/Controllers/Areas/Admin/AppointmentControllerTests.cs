using LAAuto.Services.Appointments;
using LAAuto.Services.Services;
using LAAuto.Services.Users;
using LAAuto.Web.Areas.Admin.Controllers;
using LAAuto.Web.Models.Appointments;
using LAAuto.Web.Models.Users;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.ComponentModel;

namespace LAAuto.Tests.Controllers.Areas.Admin
{
    [Category("Appointments")]
    public class AppointmentControllerTests
    {
        private readonly MockRepository _mockRepository;

        public AppointmentControllerTests()
        {
            _mockRepository = new MockRepository(MockBehavior.Loose);
        }

        [Fact]
        public async Task ListAppointments_ReturnsViewWithAppointments()
        {
            #region Arrange

            Mock<IAppointmentService> appointmentServiceMock
                = _mockRepository.Create<IAppointmentService>();

            var serviceAppointments = new List<Appointment>()
            {
                new Appointment
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
                new Appointment
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

            var controller = new AppointmentController(appointmentServiceMock.Object);
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
        public async Task DeleteAppointment_RedirectsToListAppointments()
        {
            #region Arrange

            Mock<IAppointmentService> appointmentServiceMock
                = _mockRepository.Create<IAppointmentService>();

            appointmentServiceMock.Setup(x => x.DeleteAppointmentAsync(
                    It.IsAny<Guid>()))
                .Returns(Task.CompletedTask);

            #endregion

            #region Act

            var controller = new AppointmentController(appointmentServiceMock.Object);
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
