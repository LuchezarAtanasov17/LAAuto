using LAAuto.Web.Models.Appointments;
using Microsoft.AspNetCore.Mvc;
using LAAuto.Services.Appointments;

namespace LAAuto.Web.Areas.Admin.Controllers
{
    /// <summary>
    /// Represents appointment controller.
    /// </summary>
    public class AppointmentController : BaseController
    {
        private readonly IAppointmentService _appointmentService;

        /// <summary>
        /// Initialize new instance of <see cref="AppointmentController"/> class.
        /// </summary>
        /// <param name="appointmentService"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService ?? throw new ArgumentNullException(nameof(appointmentService));
        }

        /// <summary>
        /// Lists the appointments.
        /// </summary>
        /// <returns>the list view</returns>
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var serviceAppointments = await _appointmentService.ListAppointmentsAsync();

            var appointments = serviceAppointments
                .Select(Conversion.ConvertAppointment)
                .ToList();

            return View(appointments);
        }

        /// <summary>
        /// Deletes an appointments with specified ID.
        /// </summary>
        /// <param name="id">the appointment ID</param>
        /// <returns>redirect to list action</returns>
        public async Task<IActionResult> Delete(
            [FromRoute]
            Guid id)
        {
            await _appointmentService.DeleteAppointmentAsync(id);

            return RedirectToAction(nameof(List));
        }
    }
}
