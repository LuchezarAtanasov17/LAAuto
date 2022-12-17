using LAAuto.Services;
using LAAuto.Services.Categories;
using LAAuto.Services.Impl.Categories;
using LAAuto.Tests.Mocks;
    using ENTITIES = LAAuto.Entities.Models;


namespace LAAuto.Tests.Services
{
    public class CategoryServiceTests
    {
        [Fact]
        public async Task ListCategories_ReturnsServiceCategories()
        {
            #region Arrange


            using var data = DatabaseMock.Instance;
            data.Categories.Add(new ENTITIES.Category
            {
                Name = "TestName",
            });
            data.Categories.Add(new ENTITIES.Category
            {
                Name = "TestName2"
            });

            data.SaveChanges();

            var categoryService = new CategoryService(data);

            #endregion

            #region Act

            var result = await categoryService.ListCategoriesAsync();

            #endregion

            #region Assert

            Assert.Equal(result.Count, data.Categories.Count());
            Assert.IsType<List<Category>>(result);

            #endregion
        }

        [Fact]
        public async Task ListCategories_ReturnsServiceCategoriesByServiceId()
        {
            #region Arrange

            var id = Guid.NewGuid();

            using var data = DatabaseMock.Instance;
            data.Categories.Add(new ENTITIES.Category
            {
                Name = "TestName",
            });
            data.Categories.Add(new ENTITIES.Category
            {
                Id = id,
                Name = "TestName2"
            });

            data.SaveChanges();

            data.CategoryServices.Add(new ENTITIES.CategoryService
            {
                ServiceId = id,
                CategoryId = Guid.NewGuid(),
                Category = data.Categories.First(x => x.Name == "TestName2"),
            });

            data.SaveChanges();

            var categoryService = new CategoryService(data);

            #endregion

            #region Act

            var result = await categoryService.ListCategoriesAsync(id);

            #endregion

            #region Assert

            Assert.Equal(result.Count, 1);
            Assert.IsType<List<Category>>(result);

            #endregion
        }

        [Fact]
        public async Task GetCategory_ReturnsServiceCategory()
        {
            #region Arrange

            var id = Guid.NewGuid();

            using var data = DatabaseMock.Instance;

            data.Categories.Add(new ENTITIES.Category
            {
                Id = id,
                Name = "TestName"
            });

            data.SaveChanges();

            var categoryService = new CategoryService(data);

            #endregion

            #region Act

            var result = await categoryService.GetCategoryAsync(id);

            #endregion
            #region Assert

            Assert.NotNull(result);
            Assert.IsType<Category>(result);


            #endregion
        }

        [Fact]
        public async Task GetCategory_ThrowsIfThereIsNoCategoryWithThatId()
        {
            #region Arrange

            var wrongId = Guid.NewGuid();

            using var data = DatabaseMock.Instance;

            data.Categories.Add(new ENTITIES.Category
            {
                Name = "TestName"
            });

            data.SaveChanges();

            var categoryService = new CategoryService(data);

            #endregion

            #region Act

            #endregion
            #region Assert

            ObjectNotFoundException ex = await Assert.ThrowsAsync<ObjectNotFoundException>(async ()
                => await categoryService.GetCategoryAsync(wrongId));

            Assert.Equal(ex.Message, $"Could not find category with ID {wrongId}");


            #endregion
        }

        [Fact]
        public async Task CreateCategory_ThrowsIfCreateCategoryRequestIsNull()
        {
            #region Arrange

            using var data = DatabaseMock.Instance;

            var categoryService = new CategoryService(data);


            #endregion

            #region Act

            ArgumentNullException ex = await Assert.ThrowsAsync<ArgumentNullException>(async ()
               => await categoryService.CreateCategoryAsync(null));

            #endregion
        }

        [Fact]
        public async Task DeleteAppointment_ThrowsIfThereIsNoCategoryWithThatId()
        {
            #region Arrange

            using var data = DatabaseMock.Instance;

            var categoryService = new CategoryService(data);

            var id = Guid.NewGuid();

            #endregion

            #region Act

            ObjectNotFoundException ex = await Assert.ThrowsAsync<ObjectNotFoundException>(async ()
               => await categoryService.DeleteCategoryAsync(id));

            Assert.Equal(ex.Message, $"Could not find category with ID {id}");

            #endregion
        }

    }
}
