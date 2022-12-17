using LAAuto.Web.Areas.Admin.Controllers;
using LAAuto.Web.Models.Categories;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.ComponentModel;
using SERVICES = LAAuto.Services.Categories;

namespace LAAuto.Tests.Controllers.Areas.Admin
{
    [Category("Categories")]
    public class CategoryControllerTests
    {
        private readonly MockRepository _mockRepository;

        public CategoryControllerTests()
        {
            _mockRepository = new MockRepository(MockBehavior.Loose);
        }

        [Fact]
        public async Task ListCategories_ReturnsViewWithCategories()
        {
            #region Arrange

            Mock<SERVICES.ICategoryService> categoryServiceMock
                = _mockRepository.Create<SERVICES.ICategoryService>();

            var serviceCategories = new List<SERVICES.Category>()
            {
                new SERVICES.Category
                {
                    Id = Guid.NewGuid(),
                    Name = "Test",
                },
                new SERVICES.Category
                {
                    Id = Guid.NewGuid(),
                    Name = "Again Test",
                }
            };

            categoryServiceMock.Setup(x => x.ListCategoriesAsync(
                    It.IsAny<Guid?>()))
                .Returns(Task.FromResult(serviceCategories));

            #endregion

            #region Act

            var controller = new CategoryController(categoryServiceMock.Object);
            var result = await controller.List();

            #endregion

            #region Assert

            Assert.NotNull(result);

            var expected = result as ViewResult;

            Assert.NotNull(expected);
            Assert.IsType<List<CategoryViewModel>>(expected.Model);

            var controllerCategories = expected.Model as List<CategoryViewModel>;

            List<Guid> controllerItemIds = controllerCategories!.Select(x => x.Id).ToList();
            List<Guid> serviceItemIds = serviceCategories!.Select(x => x.Id).ToList();

            List<Guid> missingIds = serviceItemIds.Except(controllerItemIds).ToList();
            List<Guid> unexpectedIds = controllerItemIds.Except(serviceItemIds).ToList();

            Assert.Empty(missingIds);
            Assert.Empty(unexpectedIds);

            #endregion
        }

        [Fact]
        public void GetCreateCategory_ReturnViewWithModel()
        {
            #region Arrange

            Mock<SERVICES.ICategoryService> categoryServiceMock
                = _mockRepository.Create<SERVICES.ICategoryService>();

            #endregion

            #region Act

            var controller = new CategoryController(categoryServiceMock.Object);
            var result = controller.Create();

            #endregion

            #region Assert

            Assert.NotNull(result);

            var expected = result as ViewResult;

            Assert.NotNull(expected);

            var asd = expected.Model as CreateCategoryRequest;

            Assert.NotNull(asd);

            Assert.Null(asd.Name);

            #endregion
        }

        [Fact]
        public async Task CreateCategory_RedirectsToListCategories()
        {
            #region Arrange

            Mock<SERVICES.ICategoryService> categoryServiceMock
                = _mockRepository.Create<SERVICES.ICategoryService>();

            categoryServiceMock.Setup(x => x.CreateCategoryAsync(
                    It.IsAny<SERVICES.CreateCategoryRequest>()))
                .Returns(Task.CompletedTask);

            var createRequest = new CreateCategoryRequest()
            {
                Name = "Test"
            };

            #endregion

            #region Act

            var controller = new CategoryController(categoryServiceMock.Object);
            var result = await controller.Create(createRequest);

            #endregion

            #region Assert

            Assert.NotNull(result);

            var expected = result as RedirectToActionResult;
            Assert.NotNull(expected);
            Assert.Equal("List", expected.ActionName);

            #endregion
        }
        [Fact]
        public void CreateCategory_ShouldReturnViewWithCorrectModelIfModelStateIsNotValid()
        {
            #region Arrange

            Mock<SERVICES.ICategoryService> categoryServiceMock
                = _mockRepository.Create<SERVICES.ICategoryService>();

            categoryServiceMock.Setup(x => x.CreateCategoryAsync(
                    It.IsAny<SERVICES.CreateCategoryRequest>()))
                .Returns(Task.CompletedTask);

            var createRequest = new CreateCategoryRequest()
            {
                Name = "Ts"
            };

            #endregion

            #region Act

            var controller = new CategoryController(categoryServiceMock.Object);
            var result = controller.Create();

            #endregion

            #region Assert

            Assert.NotNull(result);

            var expected = result as ViewResult;

            Assert.NotNull(expected);

            var model = expected.Model as CreateCategoryRequest;
            Assert.NotNull(model);

            #endregion
        }

        [Fact]
        public async Task DeleteCategory_RedirectsToListCategories()
        {
            #region Arrange

            Mock<SERVICES.ICategoryService> categoryServiceMock
                = _mockRepository.Create<SERVICES.ICategoryService>();

            categoryServiceMock.Setup(x => x.DeleteCategoryAsync(
                    It.IsAny<Guid>()))
                .Returns(Task.CompletedTask);

            #endregion

            #region Act

            var controller = new CategoryController(categoryServiceMock.Object);
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
