using LAAuto.Services;
using LAAuto.Services.Appointments;
using LAAuto.Services.Impl.Appointments;
using LAAuto.Tests.Mocks;
using ENTITIES = LAAuto.Entities.Models;

namespace LAAuto.Tests.Services
{
    public class AppointmentServiceTests
    {
        [Fact]
        public async Task ListAppointments_ReturnsServiceAppointments()
        {
            #region Arrange

            Guid userId = Guid.NewGuid();

            using var data = DatabaseMock.Instance;
            data.Appointments.Add(new ENTITIES.Appointment
            {
                Id = userId,
                CategoryId = Guid.NewGuid(),
                ServiceId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                Service = new ENTITIES.Service
                {
                    Id = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Name = "Test",
                    Location = "TestLocation",
                    OpenTime = TimeOnly.MinValue,
                    CloseTime = TimeOnly.MaxValue,
                },
                User = new ENTITIES.User
                {
                    UserName = "Test",
                    FirstName = "Test",
                    LastName = "Test",
                    Email = "Test@gmail.com",
                    PhoneNumber = "0896020441"
                }
            });
            data.Appointments.Add(new ENTITIES.Appointment
            {
                Id = Guid.NewGuid(),
                CategoryId = Guid.NewGuid(),
                ServiceId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                Service = new ENTITIES.Service
                {
                    Id = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Name = "Test2",
                    Location = "TestLocation2",
                    OpenTime = TimeOnly.MinValue,
                    CloseTime = TimeOnly.MaxValue,
                },
                User = new ENTITIES.User
                {
                    UserName = "Test2",
                    FirstName = "Test2",
                    LastName = "Test2",
                    Email = "Test2@gmail.com",
                    PhoneNumber = "0896020442"
                }
            });

            data.SaveChanges();

            var appointmentService = new AppointmentService(data);

            #endregion

            #region Act

            var result = await appointmentService.ListAppointmentsAsync();

            #endregion

            #region Assert

            Assert.Equal(result.Count, data.Appointments.Count());
            Assert.IsType<List<Appointment>>(result);

            #endregion
        }
        [Fact]
        public async Task ListAppointments_ReturnsServiceAppointmentsByUser()
        {
            #region Arrange

            Guid userId = Guid.NewGuid();

            using var data = DatabaseMock.Instance;
            data.Appointments.Add(new ENTITIES.Appointment
            {
                Id = Guid.NewGuid(),
                CategoryId = Guid.NewGuid(),
                ServiceId = Guid.NewGuid(),
                UserId = userId,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                Service = new ENTITIES.Service
                {
                    Id = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Name = "Test",
                    Location = "TestLocation",
                    OpenTime = TimeOnly.MinValue,
                    CloseTime = TimeOnly.MaxValue,
                },
                User = new ENTITIES.User
                {
                    UserName = "Test",
                    FirstName = "Test",
                    LastName = "Test",
                    Email = "Test@gmail.com",
                    PhoneNumber = "0896020441"
                }
            });
            data.Appointments.Add(new ENTITIES.Appointment
            {
                Id = Guid.NewGuid(),
                CategoryId = Guid.NewGuid(),
                ServiceId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                Service = new ENTITIES.Service
                {
                    Id = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Name = "Test2",
                    Location = "TestLocation2",
                    OpenTime = TimeOnly.MinValue,
                    CloseTime = TimeOnly.MaxValue,
                },
                User = new ENTITIES.User
                {
                    UserName = "Test2",
                    FirstName = "Test2",
                    LastName = "Test2",
                    Email = "Test2@gmail.com",
                    PhoneNumber = "0896020442"
                }
            });

            data.SaveChanges();

            var appointmentService = new AppointmentService(data);

            #endregion

            #region Act

            var result = await appointmentService.ListAppointmentsAsync(userId: userId);

            #endregion

            #region Assert

            Assert.Equal(result.Count, data.Appointments.Where(x => x.UserId == userId).Count());
            Assert.IsType<List<Appointment>>(result);

            #endregion
        }
        [Fact]
        public async Task ListAppointments_ReturnsServiceAppointmentsByService()
        {
            #region Arrange

            Guid serviceId = Guid.NewGuid();

            using var data = DatabaseMock.Instance;
            data.Appointments.Add(new ENTITIES.Appointment
            {
                Id = Guid.NewGuid(),
                CategoryId = Guid.NewGuid(),
                ServiceId = serviceId,
                UserId = Guid.NewGuid(),
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                Service = new ENTITIES.Service
                {
                    Id = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Name = "Test",
                    Location = "TestLocation",
                    OpenTime = TimeOnly.MinValue,
                    CloseTime = TimeOnly.MaxValue,
                },
                User = new ENTITIES.User
                {
                    UserName = "Test",
                    FirstName = "Test",
                    LastName = "Test",
                    Email = "Test@gmail.com",
                    PhoneNumber = "0896020441"
                }
            });
            data.Appointments.Add(new ENTITIES.Appointment
            {
                Id = Guid.NewGuid(),
                CategoryId = Guid.NewGuid(),
                ServiceId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                Service = new ENTITIES.Service
                {
                    Id = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Name = "Test2",
                    Location = "TestLocation2",
                    OpenTime = TimeOnly.MinValue,
                    CloseTime = TimeOnly.MaxValue,
                },
                User = new ENTITIES.User
                {
                    UserName = "Test2",
                    FirstName = "Test2",
                    LastName = "Test2",
                    Email = "Test2@gmail.com",
                    PhoneNumber = "0896020442"
                }
            });

            data.SaveChanges();

            var appointmentService = new AppointmentService(data);

            #endregion

            #region Act

            var result = await appointmentService.ListAppointmentsAsync(serviceId: serviceId);

            #endregion

            #region Assert

            Assert.Equal(result.Count, data.Appointments.Where(x => x.ServiceId == serviceId).Count());
            Assert.IsType<List<Appointment>>(result);

            #endregion
        }

        [Fact]
        public async Task GetAppointment_ReturnsServiceAppointment()
        {
            #region Arrange

            var id = Guid.NewGuid();

            using var data = DatabaseMock.Instance;

            data.Appointments.Add(new ENTITIES.Appointment
            {
                Id = id,
                CategoryId = Guid.NewGuid(),
                ServiceId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                Service = new ENTITIES.Service
                {
                    Id = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Name = "Test",
                    Location = "TestLocation",
                    OpenTime = TimeOnly.MinValue,
                    CloseTime = TimeOnly.MaxValue,
                },
                User = new ENTITIES.User
                {
                    UserName = "Test",
                    FirstName = "Test",
                    LastName = "Test",
                    Email = "Test@gmail.com",
                    PhoneNumber = "0896020441"
                }
            });

            data.SaveChanges();

            var appointmentService = new AppointmentService(data);

            #endregion

            #region Act

            var result = await appointmentService.GetAppointmentAsync(id);

            #endregion
            #region Assert

            Assert.NotNull(result);
            Assert.IsType<Appointment>(result);

            #endregion
        }

        [Fact]
        public async Task GetAppointment_ThrowsIfThereIsNoAppointmentWithThatId()
        {
            #region Arrange

            var id = Guid.NewGuid();
            var wrongId = Guid.NewGuid();

            using var data = DatabaseMock.Instance;

            data.Appointments.Add(new ENTITIES.Appointment
            {
                Id = id,
                CategoryId = Guid.NewGuid(),
                ServiceId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                Service = new ENTITIES.Service
                {
                    Id = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Name = "Test",
                    Location = "TestLocation",
                    OpenTime = TimeOnly.MinValue,
                    CloseTime = TimeOnly.MaxValue,
                },
                User = new ENTITIES.User
                {
                    UserName = "Test",
                    FirstName = "Test",
                    LastName = "Test",
                    Email = "Test@gmail.com",
                    PhoneNumber = "0896020441"
                }
            });

            data.SaveChanges();

            var appointmentService = new AppointmentService(data);

            #endregion

            #region Act

            #endregion
            #region Assert

            ObjectNotFoundException ex = await Assert.ThrowsAsync<ObjectNotFoundException>(async ()
                => await appointmentService.GetAppointmentAsync(wrongId));

            Assert.Equal(ex.Message, $"Could not find an appointment with ID {wrongId}.");


            #endregion
        }

        [Fact]
        public async Task CreateAppointment_ThrowsIfCreateAppointmentRequestIsNull()
        {
            #region Arrange

            using var data = DatabaseMock.Instance;

            var appointmentService = new AppointmentService(data);

            #endregion

            #region Act

            ArgumentNullException ex = await Assert.ThrowsAsync<ArgumentNullException>(async ()
               => await appointmentService.CreateAppointmentAsync(null));

            #endregion
        }

        [Fact]
        public async Task CreateAppointment_DoesNotThrowIfCreateAppointmentRequestIsFulfilledCorrectly()
        {
            #region Arrange

            using var data = DatabaseMock.Instance;

            var appointmentService = new AppointmentService(data);

            var request = new CreateAppointmentRequest
            {
                CategoryId = Guid.NewGuid(),
                ServiceId= Guid.NewGuid(),  
                UserId = Guid.NewGuid(),
                StartDate= DateTime.Now,
                EndDate= DateTime.Now,
                Description = "DescriptionTestDescriptionTestDescriptionTest"
            };


            #endregion

            #region Act

            var exception = await Record.ExceptionAsync(async () => await appointmentService.CreateAppointmentAsync(request));

            #endregion

            #region Assert

            Assert.Null(exception);

            #endregion
        }

        [Fact]
        public async Task UpdateAppointment_ThrowsIfUpdateAppointmentRequestIsNull()
        {
            #region Arrange

            using var data = DatabaseMock.Instance;

            var appointmentService = new AppointmentService(data);


            #endregion

            #region Act

            ArgumentNullException ex = await Assert.ThrowsAsync<ArgumentNullException>(async ()
               => await appointmentService.UpdateAppointmentAsync(Guid.NewGuid(), null));

            #endregion
        }

        [Fact]
        public async Task UpdateAppointment_ThrowsIfThereIsNoAppointmentWithThatId()
        {
            #region Arrange

            using var data = DatabaseMock.Instance;
            var id = Guid.NewGuid();

            var request = new UpdateAppointmentRequest()
            {
                CategoryId = Guid.NewGuid(),
                ServiceId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
            };

            var appointmentService = new AppointmentService(data);


            #endregion

            #region Act

            ObjectNotFoundException ex = await Assert.ThrowsAsync<ObjectNotFoundException>(async ()
               => await appointmentService.UpdateAppointmentAsync(id, request));

            #endregion

            #region Assert

            Assert.Equal(ex.Message, $"Could not find appointment with ID {id}.");

            #endregion
        }

        [Fact]
        public async Task UpdateAppointment_DoesNotThrowIfUpdateAppointmentRequestIsFulfilledCcorrectly()
        {
            #region Arrange

            using var data = DatabaseMock.Instance;

            var appointmentService = new AppointmentService(data);
            var id = Guid.NewGuid();


            data.Appointments.Add(new ENTITIES.Appointment()
            {
                Id = id,
                CategoryId = Guid.NewGuid(),
                ServiceId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                Description = "DescriptionTestDescriptionTestDescriptionTest",
                Category = new ENTITIES.Category() { Name = "TestName"},
                Service= new ENTITIES.Service()
                {
                    Name = "Name",
                    Location = "LocationTest"
                },
                User = new ENTITIES.User()
            });

            data.SaveChanges();

            var request = new UpdateAppointmentRequest
            {
                CategoryId = Guid.NewGuid(),
                ServiceId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                Description = "DescriptionTestDescriptionTestDescriptionTest"
            };


            #endregion

            #region Act

            var exception = await Record.ExceptionAsync(async () => await appointmentService.UpdateAppointmentAsync(id, request));

            #endregion

            #region Assert

            Assert.Null(exception);

            #endregion
        }

        [Fact]
        public async Task DeleteAppointment_ThrowsIfThereIsNoAppointmentWithThatId()
        {
            #region Arrange

            using var data = DatabaseMock.Instance;
            var id = Guid.NewGuid();

            var appointmentService = new AppointmentService(data);

            #endregion

            #region Act

            ObjectNotFoundException ex = await Assert.ThrowsAsync<ObjectNotFoundException>(async ()
               => await appointmentService.DeleteAppointmentAsync(id));

            #endregion

            #region Assert

            Assert.Equal(ex.Message, $"Could not find an appointment with ID {id}.");

            #endregion
        }

        [Fact]
        public async Task DeleteAppointment_DoesNotThrowIfThereIsAppointmentWithGivenId()
        {
            #region Arrange

            using var data = DatabaseMock.Instance;

            var appointmentService = new AppointmentService(data);

            var id = Guid.NewGuid();

            data.Appointments.Add(new ENTITIES.Appointment()
            {
                Id = id,
                CategoryId = Guid.NewGuid(),
                ServiceId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                Description = "DescriptionTestDescriptionTestDescriptionTest",
                Category = new ENTITIES.Category() { Name = "TestName" },
                Service = new ENTITIES.Service()
                {
                    Name = "Name",
                    Location = "LocationTest"
                },
                User = new ENTITIES.User()
            });

            data.SaveChanges();


            #endregion

            #region Act

            var exception = await Record.ExceptionAsync(async () => await appointmentService.DeleteAppointmentAsync(id));

            #endregion

            #region Assert

            Assert.Null(exception);

            #endregion
        }
    }
}
