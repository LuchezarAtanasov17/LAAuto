using LAAuto.Web.Models.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using SERVICES = LAAuto.Services.Services;
using USERS = LAAuto.Services.Users;

namespace LAAuto.Web.Controllers
{
    [AllowAnonymous]
    public class ServiceController : Controller
    {
        private readonly SERVICES.IServiceService _serviceService;
        private readonly USERS.IUserService _userService;


        public ServiceController(SERVICES.IServiceService serviceService, USERS.IUserService userService)
        {
            _serviceService = serviceService ?? throw new ArgumentNullException(nameof(serviceService));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
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
            Guid id)
        {
            var serviceService = await _serviceService.GetServiceAsync(id);

            var service = Conversion.ConvertService(serviceService);
            //service.User = await _userService.GetUserAsync(service.UserId);

            return View("Details",service);
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody]
            CreateServiceRequest request)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var serviceReqeust = Conversion.ConvertService(request);

            serviceReqeust.UserId = Guid.Parse("BF02CA97-4DFE-4255-B000-08DAD31010DB"); //Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            await _serviceService.CreateServiceAsync(serviceReqeust);

            return Redirect(nameof(List));
        }

        [HttpPut]
        public async Task<IActionResult> Update(Guid id, UpdateServiceRequest request)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var serviceReqeust = Conversion.ConvertService(request);

            await _serviceService.UpdateServiceAsync(id, serviceReqeust);

            return Redirect(nameof(Get));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _serviceService.DeleteServiceAsync(id);

            return Redirect(nameof(List));
        }
    }
}
