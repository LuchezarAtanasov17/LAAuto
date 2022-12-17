using LAAuto.Services;
using LAAuto.Services.Categories;
using LAAuto.Services.Impl.Services;
using LAAuto.Services.Services;
using LAAuto.Tests.Mocks;
using Moq;
using ENTITIES = LAAuto.Entities.Models;

namespace LAAuto.Tests.Services
{
    public class ServiceServiceTests
    {
        [Fact]
        public async Task ListServices_ReturnsServiceSerivices()
        {
            #region Arrange
            MockRepository _mockRepository = new MockRepository(MockBehavior.Loose);
            Mock<ICategoryService> categoryServiceMock = _mockRepository.Create<ICategoryService>();

            using var data = DatabaseMock.Instance;
            data.Services.Add(new ENTITIES.Service
            {
                UserId = Guid.NewGuid(),
                Name = "Test",
                Location = "TestLocation",
                OpenTime = TimeOnly.MinValue,
                CloseTime = TimeOnly.MaxValue,
                User = new ENTITIES.User { }
            });
            data.Services.Add(new ENTITIES.Service
            {
                UserId = Guid.NewGuid(),
                Name = "Test2",
                Location = "TestLocation2",
                OpenTime = TimeOnly.MinValue,
                CloseTime = TimeOnly.MaxValue,
                User = new ENTITIES.User { }
            });

            data.SaveChanges();

            var serviceService = new ServiceService(data, categoryServiceMock.Object);

            #endregion

            #region Act

            var result = await serviceService.ListServicesAsync();

            #endregion

            #region Assert

            Assert.Equal(result.Count, data.Services.Count());
            Assert.IsType<List<Service>>(result);

            #endregion
        }

        [Fact]
        public async Task ListServices_ReturnsServiceSerivicesByUserId()
        {
            #region Arrange
            MockRepository _mockRepository = new MockRepository(MockBehavior.Loose);
            Mock<ICategoryService> categoryServiceMock = _mockRepository.Create<ICategoryService>();

            var userId = Guid.NewGuid();

            using var data = DatabaseMock.Instance;
            data.Services.Add(new ENTITIES.Service
            {
                UserId = userId,
                Name = "Test",
                Location = "TestLocation",
                OpenTime = TimeOnly.MinValue,
                CloseTime = TimeOnly.MaxValue,
                User = new ENTITIES.User { Id = userId }
            });
            data.Services.Add(new ENTITIES.Service
            {
                UserId = Guid.NewGuid(),
                Name = "Test2",
                Location = "TestLocation2",
                OpenTime = TimeOnly.MinValue,
                CloseTime = TimeOnly.MaxValue,
                User = new ENTITIES.User { }
            });

            data.SaveChanges();

            var serviceService = new ServiceService(data, categoryServiceMock.Object);

            #endregion

            #region Act

            var result = await serviceService.ListServicesAsync(userId);

            #endregion

            #region Assert

            Assert.Equal(result.Count, 1);
            Assert.IsType<List<Service>>(result);

            #endregion
        }

        [Fact]
        public async Task GetService_ReturnsServiceSerivice()
        {
            #region Arrange
            MockRepository _mockRepository = new MockRepository(MockBehavior.Loose);
            Mock<ICategoryService> categoryServiceMock = _mockRepository.Create<ICategoryService>();

            var id = Guid.NewGuid();

            using var data = DatabaseMock.Instance;
            var service = new ENTITIES.Service
            {
                Id = id,
                UserId = Guid.NewGuid(),
                Name = "Test",
                Location = "TestLocation",
                OpenTime = TimeOnly.MinValue,
                CloseTime = TimeOnly.MaxValue,
                User = new ENTITIES.User { Id = Guid.NewGuid() }
            };

            data.Services.Add(service);

            data.SaveChanges();

            var serviceService = new ServiceService(data, categoryServiceMock.Object);

            #endregion

            #region Act

            var result = await serviceService.GetServiceAsync(id);

            #endregion

            #region Assert

            Assert.Equal(result.Location, service.Location);
            Assert.Equal(result.Name, service.Name);
            Assert.Equal(result.Id, service.Id);
            Assert.Equal(result.OpenTime, service.OpenTime);
            Assert.Equal(result.CloseTime, service.CloseTime);

            Assert.IsType<Service>(result);

            #endregion
        }

        [Fact]
        public async Task GetService_ThrowsIfThereIsNoServiceWithThatId()
        {
            #region Arrange
            MockRepository _mockRepository = new MockRepository(MockBehavior.Loose);
            Mock<ICategoryService> categoryServiceMock = _mockRepository.Create<ICategoryService>();

            var id = Guid.NewGuid();

            using var data = DatabaseMock.Instance;
            var service = new ENTITIES.Service
            {
                UserId = Guid.NewGuid(),
                Name = "Test",
                Location = "TestLocation",
                OpenTime = TimeOnly.MinValue,
                CloseTime = TimeOnly.MaxValue,
                User = new ENTITIES.User { Id = Guid.NewGuid() }
            };

            data.Services.Add(service);

            data.SaveChanges();

            var serviceService = new ServiceService(data, categoryServiceMock.Object);

            #endregion

            #region Act

            #endregion

            #region Assert

            ObjectNotFoundException ex = await Assert.ThrowsAsync<ObjectNotFoundException>(async ()
               => await serviceService.GetServiceAsync(id));

            Assert.Equal(ex.Message, $"Could not find service with ID {id}.");
            #endregion
        }

        [Fact]
        public async Task CreateService_DoesNotThrowIfCreateServiceRequestIsFulfilledCorrectly()
        {
            #region Arrange
            MockRepository _mockRepository = new MockRepository(MockBehavior.Loose);
            Mock<ICategoryService> categoryServiceMock = _mockRepository.Create<ICategoryService>();
            using var data = DatabaseMock.Instance;

            var serviceService = new ServiceService(data, categoryServiceMock.Object);

            var request = new CreateServiceRequest
            {
                UserId = Guid.NewGuid(),
                OpenTime = TimeOnly.MinValue,
                CloseTime = TimeOnly.MaxValue,
                Description = "DescriptionTestDescriptionTestDescriptionTest",
                Name = "TestName",
                Location = "TestLocation",
                CategoryIds = new List<Guid>() { Guid.NewGuid() }
            };


            #endregion

            #region Act

            var exception = await Record.ExceptionAsync(async () => await serviceService.CreateServiceAsync(request));

            #endregion

            #region Assert

            Assert.Null(exception);

            #endregion
        }
        [Fact]
        public async Task CreateService_ThrowsIfCreateServiceRequestIsNull()
        {
            #region Arrange
            MockRepository _mockRepository = new MockRepository(MockBehavior.Loose);
            Mock<ICategoryService> categoryServiceMock = _mockRepository.Create<ICategoryService>();
            using var data = DatabaseMock.Instance;

            var serviceService = new ServiceService(data, categoryServiceMock.Object);

            #endregion

            #region Act

            #endregion

            #region Assert

            ArgumentNullException ex = await Assert.ThrowsAsync<ArgumentNullException>(async ()
               => await serviceService.CreateServiceAsync(null));

            #endregion
        }

        [Fact]
        public async Task UpdateService_DoesNotThrowIfUpdateServiceRequestIsFulfilledCorrectly()
        {
            #region Arrange
            MockRepository _mockRepository = new MockRepository(MockBehavior.Loose);
            Mock<ICategoryService> categoryServiceMock = _mockRepository.Create<ICategoryService>();
            using var data = DatabaseMock.Instance;

            var serviceService = new ServiceService(data, categoryServiceMock.Object);
            var id = Guid.NewGuid();


            data.Services.Add(new ENTITIES.Service()
            {
                Id = id,
                OpenTime = TimeOnly.MinValue,
                CloseTime = TimeOnly.MaxValue,
                Description = "DescriptionTestDescriptionTestDescriptionTest",
                Name = "TestName",
                Location = "TestLocation",
            });

            data.SaveChanges();

            var request = new UpdateServiceRequest
            {
                OpenTime = TimeOnly.MinValue,
                CloseTime = TimeOnly.MaxValue,
                Description = "DescriptionTestDescriptionTestDescriptionTest",
                Name = "TestName",
                Location = "TestLocation",
                Categories = new List<Category>()
                {
                    new Category()
                    {
                        Id = Guid.NewGuid(),
                        Name = "TestName",
                    }
                }
            };

            #endregion

            #region Act

            var exception = await Record.ExceptionAsync(async () => await serviceService.UpdateServiceAsync(id, request));

            #endregion

            #region Assert

            Assert.Null(exception);

            #endregion
        }

        [Fact]
        public async Task UpdateService_ThrowsIfUpdateServiceRequestIsNull()
        {
            #region Arrange
            MockRepository _mockRepository = new MockRepository(MockBehavior.Loose);
            Mock<ICategoryService> categoryServiceMock = _mockRepository.Create<ICategoryService>();
            using var data = DatabaseMock.Instance;

            var serviceService = new ServiceService(data, categoryServiceMock.Object);

            #endregion

            #region Act

            #endregion

            #region Assert

            ArgumentNullException ex = await Assert.ThrowsAsync<ArgumentNullException>(async ()
               => await serviceService.UpdateServiceAsync(Guid.NewGuid(), null));

            #endregion
        }

        [Fact]
        public async Task UpdateService_ThrowsIfThereIsNoServiceWithGivenId()
        {
            #region Arrange
            MockRepository _mockRepository = new MockRepository(MockBehavior.Loose);
            Mock<ICategoryService> categoryServiceMock = _mockRepository.Create<ICategoryService>();
            using var data = DatabaseMock.Instance;

            var id = Guid.NewGuid();

            data.Services.Add(new ENTITIES.Service()
            {
                Id = Guid.NewGuid(),
                OpenTime = TimeOnly.MinValue,
                CloseTime = TimeOnly.MaxValue,
                Description = "DescriptionTestDescriptionTestDescriptionTest",
                Name = "TestName",
                Location = "TestLocation",
            });

            data.SaveChanges();

            var request = new UpdateServiceRequest
            {
                OpenTime = TimeOnly.MinValue,
                CloseTime = TimeOnly.MaxValue,
                Description = "DescriptionTestDescriptionTestDescriptionTest",
                Name = "TestName",
                Location = "TestLocation",
                Categories = new List<Category>()
                {
                    new Category()
                    {
                        Id = Guid.NewGuid(),
                        Name = "TestName",
                    }
                }
            };

            var serviceService = new ServiceService(data, categoryServiceMock.Object);

            #endregion

            #region Act

            #endregion

            #region Assert

            ObjectNotFoundException ex = await Assert.ThrowsAsync<ObjectNotFoundException>(async ()
               => await serviceService.UpdateServiceAsync(id, request));

            Assert.Equal(ex.Message, $"Could not find service with ID {id}.");
            #endregion
        }


        [Fact]
        public async Task UpdateRating_DoesNotThrowIfUpdateRatingRequestIsFulfilledCorrectly()
        {
            #region Arrange
            MockRepository _mockRepository = new MockRepository(MockBehavior.Loose);
            Mock<ICategoryService> categoryServiceMock = _mockRepository.Create<ICategoryService>();
            using var data = DatabaseMock.Instance;

            var serviceService = new ServiceService(data, categoryServiceMock.Object);
            var id = Guid.NewGuid();

            var request = new UpdateRatingRequest
            {
            };

            #endregion

            #region Act

            var exception = await Record.ExceptionAsync(async () => await serviceService.UpdateServiceRatingAsync(request));

            #endregion

            #region Assert

            Assert.Null(exception);

            #endregion
        }

        [Fact]
        public async Task UpdateServiceRating_ThrowsIfUpdateRatingRequestIsNull()
        {
            #region Arrange
            MockRepository _mockRepository = new MockRepository(MockBehavior.Loose);
            Mock<ICategoryService> categoryServiceMock = _mockRepository.Create<ICategoryService>();
            using var data = DatabaseMock.Instance;

            var serviceService = new ServiceService(data, categoryServiceMock.Object);

            #endregion

            #region Act

            #endregion

            #region Assert

            ArgumentNullException ex = await Assert.ThrowsAsync<ArgumentNullException>(async ()
               => await serviceService.UpdateServiceRatingAsync(null));

            #endregion
        }

        [Fact]
        public async Task DeleteService_DoesNotThrowIfThereIsServiceWithGivenId()
        {
            #region Arrange
            MockRepository _mockRepository = new MockRepository(MockBehavior.Loose);
            Mock<ICategoryService> categoryServiceMock = _mockRepository.Create<ICategoryService>();
            using var data = DatabaseMock.Instance;

            var serviceService = new ServiceService(data, categoryServiceMock.Object);
            var id = Guid.NewGuid();

            data.Services.Add(new ENTITIES.Service()
            {
                Id = id,
                OpenTime = TimeOnly.MinValue,
                CloseTime = TimeOnly.MaxValue,
                Description = "DescriptionTestDescriptionTestDescriptionTest",
                Name = "TestName",
                Location = "TestLocation",
            });

            data.SaveChanges();

            #endregion

            #region Act

            var exception = await Record.ExceptionAsync(async () => await serviceService.DeleteServiceAsync(id));

            #endregion

            #region Assert

            Assert.Null(exception);

            #endregion
        }
    }
}
