using LAAuto.Entities.Data;
using LAAuto.Services.Appointments;
using LAAuto.Services.Services;
using LAAuto.Services.Users;
using Microsoft.EntityFrameworkCore;
using ENTITIES = LAAuto.Entities.Models;

namespace LAAuto.Services.Impl.Appointments
{
    public class AppointmentService : IAppointmentService
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;
        private readonly IServiceService _serviceService;


        public AppointmentService(
            ApplicationDbContext context,
            IUserService userService,
            IServiceService serviceService)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _serviceService = serviceService ?? throw new ArgumentNullException(nameof(serviceService));
        }

        public async Task<List<Appointment>> ListAppointmentsAsync()
        {
            List<ENTITIES.Appointment> entities = await _context.Appointments.ToListAsync();

            List<Appointment> appointments = new List<Appointment>();

            foreach (var entity in entities)
            {
                appointments.Add(await GetAppointmentAsync(entity.Id));
            }

            return appointments;
        }

        public async Task<Appointment> GetAppointmentAsync(Guid id)
        {
            var entity = await _context.Appointments.FirstOrDefaultAsync(x => x.Id == id);

            if (entity is null)
            {
                throw new ObjectNotFoundException($"Could not find an appointment with ID {id}.");
            }

            var appointment = Conversion.ConvertAppointment(entity);
            appointment.User = await _userService.GetUserAsync(appointment.UserId);
            appointment.Service = await _serviceService.GetServiceAsync(appointment.ServiceId);

            return appointment;
        }

        public async Task CreateAppointmentAsync(CreateAppointmentRequest request)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var entity = Conversion.ConvertAppointment(request);

            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAppointmentAsync(Guid id, UpdateAppointmentRequest request)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var entity = await _context.Appointments.FirstOrDefaultAsync(x => x.Id == id);

            if (entity is null)
            {
                throw new ObjectNotFoundException($"Could not find appointment with ID {id}.");
            }

            entity.UserId = request.UserId;
            entity.CategoryId = request.CategoryId;
            entity.UserId = request.UserId;
            entity.StartDate = request.StartDate;
            entity.EndDate = request.EndDate;
            entity.Description = request.Description;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAppointmentAsync(Guid id)
        {
            var entity = await _context.Appointments
                .FirstOrDefaultAsync(x => x.Id == id);

            if (entity is null)
            {
                throw new ObjectNotFoundException($"Could not find an appointment with ID {id}");
            }

            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
