using LAAuto.Web.Models.Services;
using Microsoft.AspNetCore.Mvc;
using SERVICES = LAAuto.Services.Services;

namespace LAAuto.Web.Areas.Admin.Controllers
{
    public class ServiceController : BaseController
    {
        private readonly SERVICES.IServiceService _serviceService;
        private readonly IWebHostEnvironment _hostEnvironment;


        public ServiceController(
            SERVICES.IServiceService serviceService,
            IWebHostEnvironment hostEnvironment)
        {
            _serviceService = serviceService ?? throw new ArgumentNullException(nameof(serviceService));
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

        public async Task<IActionResult> Delete(
            [FromRoute]
            Guid id)
        {
            await _serviceService.DeleteServiceAsync(id);

            return RedirectToAction(nameof(List));
        }
    }
}
