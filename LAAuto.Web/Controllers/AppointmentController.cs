using SERVICES = LAAuto.Services.Services;
using CATEGORIES = LAAuto.Services.Categories;
using APPOINTMENTS = LAAuto.Services.Appointments;
using Microsoft.AspNetCore.Mvc;
using LAAuto.Web.Models.Appointments;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using SERVICES_MODELS = LAAuto.Web.Models.Services;
using CATEGORIES_MODELS = LAAuto.Web.Models.Categories;
using Microsoft.EntityFrameworkCore;

namespace LAAuto.Web.Controllers
{
    [Authorize]
    public class AppointmentController : Controller
    {
        private readonly APPOINTMENTS.IAppointmentService _appointmentService;
        private readonly SERVICES.IServiceService _serviceService;
        private readonly CATEGORIES.ICategoryService _categoryService;

        public AppointmentController(APPOINTMENTS.IAppointmentService appointmentService, SERVICES.IServiceService serviceService, CATEGORIES.ICategoryService categoryService)
        {
            _appointmentService = appointmentService ?? throw new ArgumentNullException(nameof(appointmentService));
            _serviceService = serviceService ?? throw new ArgumentNullException(nameof(serviceService));
            _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));

        }

        [HttpGet]
        [AllowAnonymous] // ?????????????????????
        public async Task<IActionResult> List()
        {
            var serviceAppointments = await _appointmentService.ListAppointmentsAsync();

            var appointments = serviceAppointments
                .Select(Conversion.ConvertAppointment)
                .ToList();

            return View(appointments);
        }

        [HttpGet]
        [AllowAnonymous] // ?????????????????????
        public async Task<IActionResult> Get(
            Guid id)
        {
            var serviceAppointment = await _appointmentService.GetAppointmentAsync(id);

            var appointment = Conversion.ConvertAppointment(serviceAppointment);

            return View(appointment);
        }

        [HttpGet]
        public async Task<IActionResult> Create(
            Guid id)
        {
            var model = new CreateAppointmentRequest() 
            {
                ServiceId = id,
            };

            var service = await _serviceService.GetServiceAsync(id);

            model.Service = SERVICES_MODELS.Conversion.ConvertService(service);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create2(
            Guid id,
            CreateAppointmentRequest request)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            request.UserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            request.StartDate = request.StartDate.AddHours(request.StartDateHour);
            request.EndDate = request.StartDate.AddHours(1);

            var appointmentRequest = Conversion.ConvertCreateAppointmentRequest(request);
            appointmentRequest.ServiceId= id;
            
            await _appointmentService.CreateAppointmentAsync(appointmentRequest);

            return RedirectToAction(nameof(List));
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var appointment = await _appointmentService.GetAppointmentAsync(id);
            var service = await _serviceService.GetServiceAsync(appointment.ServiceId);

            var model = new UpdateAppointmentRequest()
            {
                Id = appointment.Id,
                UserId = appointment.UserId,
                ServiceId = appointment.ServiceId,
                Description = appointment.Description,
                StartDate = appointment.StartDate,
                EndDate = appointment.EndDate,
                Service = SERVICES_MODELS.Conversion.ConvertService(service)
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Guid id, UpdateAppointmentRequest request)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var serviceRequest = Conversion.ConvertUpdateAppointmentRequest(request);
            serviceRequest.UserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            serviceRequest.StartDate = request.StartDate.AddHours(request.StartDateHour);
            serviceRequest.EndDate = serviceRequest.StartDate.AddHours(1);

            await _appointmentService.UpdateAppointmentAsync(id, serviceRequest);

            return RedirectToAction(nameof(List));
        }

        public async Task<IActionResult> Delete(
            [FromRoute]
            Guid id)
        {
            await _appointmentService.DeleteAppointmentAsync(id);

            return RedirectToAction(nameof(List));
        }
    }
}
