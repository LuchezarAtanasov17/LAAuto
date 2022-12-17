using LAAuto.Web.Models.Appointments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using APPOINTMENTS = LAAuto.Services.Appointments;
using CATEGORIES = LAAuto.Services.Categories;
using CATEGORIES_MODELS = LAAuto.Web.Models.Categories;
using SERVICES = LAAuto.Services.Services;
using SERVICES_MODELS = LAAuto.Web.Models.Services;

namespace LAAuto.Web.Controllers
{
    [Authorize]
    public class AppointmentController : Controller
    {
        private readonly APPOINTMENTS.IAppointmentService _appointmentService;
        private readonly SERVICES.IServiceService _serviceService;
        private readonly CATEGORIES.ICategoryService _categoryService;

        public AppointmentController(
            APPOINTMENTS.IAppointmentService appointmentService, 
            SERVICES.IServiceService serviceService, 
            CATEGORIES.ICategoryService categoryService)
        {
            _appointmentService = appointmentService ?? throw new ArgumentNullException(nameof(appointmentService));
            _serviceService = serviceService ?? throw new ArgumentNullException(nameof(serviceService));
            _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));

        }

        [HttpGet]
        public async Task<IActionResult> List(
            Guid? serviceId = null,
            Guid? userId = null)
        {
            Guid? userGuid = userId == null
                ? null
                : userId;

            var serviceAppointments = await _appointmentService.ListAppointmentsAsync(userId: userGuid, serviceId: serviceId);

            List<AppointmentViewModel> appointments = serviceAppointments
                .Select(Conversion.ConvertAppointment)
                .ToList();

            if (userId != null)
            {
                return View("ListByUser", appointments);
            }

            return View("ListByService", appointments);
        }

        [HttpGet]
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
        public async Task<IActionResult> Create(
            Guid serviceId,
            CreateAppointmentRequest request)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            var service = await _serviceService.GetServiceAsync(serviceId);

            request.ServiceId = serviceId;
            request.Service = SERVICES_MODELS.Conversion.ConvertService(service);

            request.UserId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            request.StartDate = request.StartDate.AddHours(request.StartDateHour);
            request.EndDate = request.StartDate.AddHours(1);

            var category = await _categoryService.GetCategoryAsync(request.CategoryId);
            request.Category = CATEGORIES_MODELS.Conversion.ConvertCategory(category);

            if (request.StartDate < DateTime.Now)
            {
                ModelState.AddModelError(nameof(request.StartDate), "Invalid date.");
            }

            if (!ModelState.IsValid)
            {
                return View("Create", request);
            }

            var appointmentRequest = Conversion.ConvertCreateAppointmentRequest(request);
            appointmentRequest.ServiceId = serviceId;

            await _appointmentService.CreateAppointmentAsync(appointmentRequest);

            return RedirectToAction(nameof(List), new { userId = appointmentRequest.UserId });
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


            var service = await _serviceService.GetServiceAsync(request.ServiceId);
            request.Service = SERVICES_MODELS.Conversion.ConvertService(service);

            serviceRequest.UserId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            serviceRequest.StartDate = request.StartDate.AddHours(request.StartDateHour);
            serviceRequest.EndDate = serviceRequest.StartDate.AddHours(1);

            if (request.StartDate < DateTime.Now)
            {
                ModelState.AddModelError(nameof(request.StartDate), "Invalid date.");
            }

            if (!ModelState.IsValid)
            {
                return View("Update", request);
            }

            await _appointmentService.UpdateAppointmentAsync(id, serviceRequest);

            return RedirectToAction(nameof(List), new { userId = serviceRequest.UserId });
        }

        public async Task<IActionResult> Delete(
            [FromRoute]
            Guid id)
        {
            await _appointmentService.DeleteAppointmentAsync(id);

            return RedirectToAction(nameof(List));
        }

        public async Task<IActionResult> DeleteMyAppointment(
           [FromRoute]
            Guid id)
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            await _appointmentService.DeleteAppointmentAsync(id);

            return RedirectToAction(nameof(List), new {userId = userId});
        }
    }
}
