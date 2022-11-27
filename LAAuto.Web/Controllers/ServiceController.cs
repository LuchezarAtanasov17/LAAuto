using LAAuto.Web.Models.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SERVICES = LAAuto.Services.Services;

namespace LAAuto.Web.Controllers
{
    [AllowAnonymous]
    public class ServiceController : Controller
    {
        private readonly SERVICES.IServiceService _serviceService;

        public ServiceController(SERVICES.IServiceService serviceService)
        {
            _serviceService = serviceService ?? throw new ArgumentNullException(nameof(serviceService));
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
        public async Task<IActionResult> Get(
            [FromQuery]
            Guid id)
        {
            var serviceService = await _serviceService.GetServiceAsync(id);

            var service = Conversion.ConvertService(serviceService);

            return View(service);
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
