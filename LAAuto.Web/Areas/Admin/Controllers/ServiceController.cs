using LAAuto.Web.Models.Services;
using Microsoft.AspNetCore.Mvc;
using SERVICES = LAAuto.Services.Services;

namespace LAAuto.Web.Areas.Admin.Controllers
{
    /// <summary>
    /// Represents service controller.
    /// </summary>
    public class ServiceController : BaseController
    {
        private readonly SERVICES.IServiceService _serviceService;
        private readonly IWebHostEnvironment _hostEnvironment;


        /// <summary>
        /// Initialize new instance of <see cref="ServiceController"/> class.
        /// </summary>
        /// <param name="serviceService"></param>
        /// <param name="hostEnvironment"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public ServiceController(
            SERVICES.IServiceService serviceService,
            IWebHostEnvironment hostEnvironment)
        {
            _serviceService = serviceService ?? throw new ArgumentNullException(nameof(serviceService));
            _hostEnvironment = hostEnvironment ?? throw new ArgumentNullException(nameof(hostEnvironment));
        }

        /// <summary>
        /// Lists the services.
        /// </summary>
        /// <returns>the list view</returns>
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var serviceServices = await _serviceService.ListServicesAsync();

            var services = serviceServices
                .Select(Conversion.ConvertService)
                .ToList();

            return View(services);
        }

        /// <summary>
        /// Deletes service with specified ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Redirects to list action</returns>
        public async Task<IActionResult> Delete(
            [FromRoute]
            Guid id)
        {
            await _serviceService.DeleteServiceAsync(id);

            return RedirectToAction(nameof(List));
        }
    }
}
