using SERVICES = LAAuto.Services.Appointments;
using Microsoft.AspNetCore.Mvc;
using LAAuto.Web.Models.Appointments;
using Microsoft.AspNetCore.Authorization;

namespace LAAuto.Web.Controllers
{
    [Authorize]
    public class AppointmentController : Controller
    {
        private readonly SERVICES.IAppointmentService _appointmentService;

        public AppointmentController(SERVICES.IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService ?? throw new ArgumentNullException(nameof(appointmentService));
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
            [FromQuery]
            Guid id)
        {
            var serviceAppointment = await _appointmentService.GetAppointmentAsync(id);

            var appointment = Conversion.ConvertAppointment(serviceAppointment);

            return View(appointment);
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody]
            CreateAppointmentRequest request)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var appointmentRequest = Conversion.ConvertCreateAppointmentRequest(request);

            await _appointmentService.CreateAppointmentAsync(appointmentRequest);

            return Redirect(nameof(List));
        }

        [HttpPut]
        public async Task<IActionResult> Update(Guid id, UpdateAppointmentRequest request)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var serviceRequest = Conversion.ConvertUpdateAppointmentRequest(request);

            await _appointmentService.UpdateAppointmentAsync(id, serviceRequest);

            return Redirect(nameof(Get));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _appointmentService.DeleteAppointmentAsync(id);

            return Redirect(nameof(List));
        }
    }
}
