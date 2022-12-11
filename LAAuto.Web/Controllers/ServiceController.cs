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
    [AllowAnonymous]
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

            var services = serviceServices
                .Select(Conversion.ConvertService)
                .ToList();

            return View(services);
        }

        [HttpGet]
        public async Task<IActionResult> Mine()
        {
            Guid userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var serviceServices = await _serviceService.ListMyServicesAsync(userId);

            var services = serviceServices.Select(Conversion.ConvertService).ToList();

            //TODO:
            if (services.Count == 0)
            {
                return RedirectToAction(nameof(List));
            }

            return View(serviceServices);
        }

        [HttpGet]
        public async Task<IActionResult> Get(
            [FromRoute]
            Guid id)
        {
            var serviceService = await _serviceService.GetServiceAsync(id);

            var service = Conversion.ConvertService(serviceService);
            //service.User = await _userService.GetUserAsync(service.UserId);

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

            if (!ModelState.IsValid)
            {
                return View(request);
            }

            request.UserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var serviceRequest = Conversion.ConvertService(request);

            serviceRequest.CategoryIds = request.Categories
                .Where(x => x.IsSelected)
                .Select(x => x.Id)
                .ToList();

            await _serviceService.CreateServiceAsync(serviceRequest);

            return RedirectToAction(nameof(List));
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
            //if (!ModelState.IsValid)
            //{
            //    return View(request);
            //}
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var serviceRequest = Conversion.ConvertService(request);

            serviceRequest.Categories = request.Categories
                .Where(x=>x.IsSelected == true)
                .Select(CATEGORIES_MODELS.Conversion.ConvertSelectCategory)
                .ToList();

            await _serviceService.UpdateServiceAsync(id, serviceRequest);

            return RedirectToAction(nameof(Get), new {Id = id});
        }

        public async Task<IActionResult> Delete(
            [FromRoute]
            Guid id)
        {
            await _serviceService.DeleteServiceAsync(id);

            return RedirectToAction(nameof(List));
        }
    }
}
