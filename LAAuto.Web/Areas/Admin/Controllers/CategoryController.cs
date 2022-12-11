using Microsoft.AspNetCore.Mvc;
using SERVICES = LAAuto.Services.Categories;
using Microsoft.AspNetCore.Authorization;
using LAAuto.Web.Models.Categories;

namespace LAAuto.Web.Areas.Admin.Controllers
{
    public class CategoryController : BaseController
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
        public IActionResult Create()
        {
            var model = new CreateCategoryRequest();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(
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

        public async Task<IActionResult> Delete(
            [FromRoute]
            Guid id)
        {
            await _categoryService.DeleteCategoryAsync(id);

            return RedirectToAction(nameof(List));
        }
    }
}
