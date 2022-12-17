using LAAuto.Web.Models.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Immutable;
using System.Security.Claims;
using CATEGORIES_MODELS = LAAuto.Web.Models.Categories;
using SERVICE_CATEGORIES = LAAuto.Services.Categories;
using SERVICE_USERS = LAAuto.Services.Users;
using SERVICES = LAAuto.Services.Services;

namespace LAAuto.Web.Controllers
{
    [Authorize]
    public class ServiceController : Controller
    {
        private readonly SERVICES.IServiceService _serviceService;
        private readonly SERVICE_USERS.IUserService _userService;
        private readonly SERVICE_CATEGORIES.ICategoryService _categoryService;
        private readonly IWebHostEnvironment _hostEnvironment;


        public ServiceController(
            SERVICES.IServiceService serviceService,
            SERVICE_USERS.IUserService userService,
            SERVICE_CATEGORIES.ICategoryService categoryService,
            IWebHostEnvironment hostEnvironment)
        {
            _serviceService = serviceService ?? throw new ArgumentNullException(nameof(serviceService));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
            _hostEnvironment = hostEnvironment ?? throw new ArgumentNullException(nameof(hostEnvironment));
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var serviceServices = await _serviceService.ListServicesAsync();

            serviceServices = serviceServices.Where(x => x.UserId != Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier))).ToList();

            var services = serviceServices
                .Select(Conversion.ConvertService)
                .ToList();

            return View(services);
        }

        [HttpGet]
        public async Task<IActionResult> Mine()
        {
            Guid userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var serviceServices = await _serviceService.ListServicesAsync(userId);

            var services = serviceServices
                .Select(Conversion.ConvertService)
                .ToList();

            return View("List", services);
        }

        [HttpGet]
        public async Task<IActionResult> Get(
            [FromRoute]
            Guid id)
        {
            var serviceService = await _serviceService.GetServiceAsync(id);

            var service = Conversion.ConvertService(serviceService);

            return View("Details", service);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var categories = await _categoryService.ListCategoriesAsync();

            var model = new CreateServiceRequest()
            {
                Categories = categories
                    .Select(CATEGORIES_MODELS.Conversion.ConvertSelectCategory)
                    .ToList(),
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            CreateServiceRequest request)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (request.Categories.Where(x => x.IsSelected == true).ToList().Count < 1)
            {
                ModelState.AddModelError(nameof(request.Categories), "You should select at least one category.");
            }
            if (request.OpenTime == request.CloseTime || DateTime.Parse(request.CloseTime) < DateTime.Parse(request.OpenTime))
            {
                ModelState.AddModelError(nameof(request.CloseTime), "You should select correct open and close times.");
            }

            if (!ModelState.IsValid)
            {
                return View(request);
            }

            request.UserId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var serviceRequest = Conversion.ConvertService(request);

            serviceRequest.CategoryIds = request.Categories
                .Where(x => x.IsSelected)
                .Select(x => x.Id)
                .ToList();

            await _serviceService.CreateServiceAsync(serviceRequest);

            return RedirectToAction(nameof(Mine));
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var service = await _serviceService.GetServiceAsync(id);
            var categories = await _categoryService.ListCategoriesAsync();

            var model = new UpdateServiceRequest()
            {
                Id = service.Id,
                Location = service.Location,
                Description = service.Description,
                Name = service.Name,
                OpenTime = service.OpenTime.ToString(),
                CloseTime = service.CloseTime.ToString(),
                Categories = categories
                    .Select(CATEGORIES_MODELS.Conversion.ConvertSelectCategory)
                    .ToList()
            };

            foreach (var category in model.Categories)
            {
                if (service.Categories.Select(x => x.Id).Contains(category.Id))
                {
                    category.IsSelected = true;
                }
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Guid id, UpdateServiceRequest request)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (request.Categories.Where(x => x.IsSelected == true).ToList().Count < 1)
            {
                ModelState.AddModelError(nameof(request.Categories), "You should select at least one category.");
            }
            if (request.OpenTime == request.CloseTime)
            {
                ModelState.AddModelError(nameof(request.CloseTime), "You should select correct open and close times.");
            }
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            var serviceRequest = Conversion.ConvertService(request);

            serviceRequest.Categories = request.Categories
                .Where(x => x.IsSelected == true)
                .Select(CATEGORIES_MODELS.Conversion.ConvertSelectCategory)
                .ToList();

            await _serviceService.UpdateServiceAsync(id, serviceRequest);

            return RedirectToAction(nameof(Get), new { Id = id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateRating(
            UpdateRatingRequest request)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (!ModelState.IsValid)
            {
                return View(request);
            }

            request.UserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var serviceRequest = Conversion.ConvertUpdateRating(request);

            await _serviceService.UpdateServiceRatingAsync(serviceRequest);

            return RedirectToAction(nameof(Get), new { id = request.ServiceId });
        }

        public async Task<IActionResult> Delete(
            [FromRoute]
            Guid id)
        {
            await _serviceService.DeleteServiceAsync(id);

            return RedirectToAction(nameof(Mine));
        }
    }
}
