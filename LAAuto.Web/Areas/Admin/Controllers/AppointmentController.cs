using LAAuto.Web.Models.Appointments;
using Microsoft.AspNetCore.Mvc;
using LAAuto.Services.Appointments;

namespace LAAuto.Web.Areas.Admin.Controllers
{
    public class AppointmentController : BaseController
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService ?? throw new ArgumentNullException(nameof(appointmentService));
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var serviceAppointments = await _appointmentService.ListAppointmentsAsync();

            var appointments = serviceAppointments
                .Select(Conversion.ConvertAppointment)
                .ToList();

            return View(appointments);
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
