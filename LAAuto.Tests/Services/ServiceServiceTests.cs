using Castle.Components.DictionaryAdapter;
using LAAuto.Services;
using LAAuto.Services.Appointments;
using LAAuto.Services.Categories;
using LAAuto.Services.Impl.Appointments;
using LAAuto.Services.Impl.Services;
using LAAuto.Services.Services;
using LAAuto.Tests.Mocks;
using Microsoft.EntityFrameworkCore;
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
                Name= "Test",
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
                User = new ENTITIES.User { Id = userId}
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

    }
}
