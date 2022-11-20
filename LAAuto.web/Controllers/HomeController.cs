using LAAuto.Services.Appointments;
using LAAuto.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LAAuto.Web.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly IAppointmentService _appointmentService;

        public HomeController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService ?? throw new ArgumentNullException(nameof(appointmentService));
        }

        [HttpGet]
        public async Task<IActionResult> ListAppointments()
        {
            List<Appointment> serviceAppointments = await _appointmentService.ListAppointmentsAsync();

            return View(serviceAppointments);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAppointment(
            [FromBody]
            CreateAppointmentRequest request)
        {
            if (request is null)
            {
                // TODO 
            }

            // convert to service request

            await _appointmentService.CreateAppointmentAsync(request);

            return Redirect("/");
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}