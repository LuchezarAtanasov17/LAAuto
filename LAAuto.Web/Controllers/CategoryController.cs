using Microsoft.AspNetCore.Mvc;
using SERVICES = LAAuto.Services.Categories;
using LAAuto.Web.Models.Categories;
using Microsoft.AspNetCore.Authorization;

namespace LAAuto.Web.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly SERVICES.ICategoryService _categoryService;

        public CategoryController(SERVICES.ICategoryService categoryService)
        {
            _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var categoryServices = await _categoryService.ListCategoriesAsync();

            var categories = categoryServices
                .Select(Conversion.ConvertCategory)
                .ToList();

            return View(categories);
        }

        [HttpGet]
        public async Task<IActionResult> Get(
           [FromQuery]
            Guid id)
        {
            var categoryService = await _categoryService.GetCategoryAsync(id);

            var category = Conversion.ConvertCategory(categoryService);

            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody]
            CreateCategoryRequest request)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var categoryReqeust = Conversion.ConvertCreateCategoryRequest(request);

            await _categoryService.CreateCategoryAsync(categoryReqeust);

            return Redirect(nameof(List));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _categoryService.DeleteCategoryAsync(id);

            return Redirect(nameof(List));
        }
    }
}
