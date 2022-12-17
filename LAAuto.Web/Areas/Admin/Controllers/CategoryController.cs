using LAAuto.Web.Models.Categories;
using Microsoft.AspNetCore.Mvc;
using SERVICES = LAAuto.Services.Categories;

namespace LAAuto.Web.Areas.Admin.Controllers
{
    /// <summary>
    /// Represents category controller.
    /// </summary>
    public class CategoryController : BaseController
    {
        private readonly SERVICES.ICategoryService _categoryService;

        /// <summary>
        /// Initialize new instance of <see cref="CategoryController"/> class.
        /// </summary>
        /// <param name="categoryService"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public CategoryController(SERVICES.ICategoryService categoryService)
        {
            _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
        }

        /// <summary>
        /// Lists the categories.
        /// </summary>
        /// <returns>the list view</returns>
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var categoryServices = await _categoryService.ListCategoriesAsync();

            var categories = categoryServices
                .Select(Conversion.ConvertCategory)
                .ToList();

            return View(categories);
        }

        /// <summary>
        /// Creates a category.
        /// </summary>
        /// <returns>the create view</returns>
        [HttpGet]
        public IActionResult Create()
        {
            var model = new CreateCategoryRequest();

            return View(model);
        }

        /// <summary>
        /// Create a category
        /// </summary>
        /// <param name="request"></param>
        /// <returns>redirects to list action</returns>
        [HttpPost]
        public async Task<IActionResult> Create(
            CreateCategoryRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            var categoryReqeust = Conversion.ConvertCreateCategoryRequest(request);

            await _categoryService.CreateCategoryAsync(categoryReqeust);

            return RedirectToAction(nameof(List));
        }

        /// <summary>
        /// Deletes a category with specified ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Redirects to list action</returns>
        public async Task<IActionResult> Delete(
            [FromRoute]
            Guid id)
        {
            await _categoryService.DeleteCategoryAsync(id);

            return RedirectToAction(nameof(List));
        }
    }
}
