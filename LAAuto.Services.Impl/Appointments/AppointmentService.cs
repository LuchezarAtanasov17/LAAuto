using LAAuto.Entities.Data;
using LAAuto.Services.Appointments;
using Microsoft.EntityFrameworkCore;
using SERVICES_IMPL_USERS = LAAuto.Services.Impl.Users;
using SERVICES_IMPL_SERVICES = LAAuto.Services.Impl.Services;

namespace LAAuto.Services.Impl.Appointments
{
    public class AppointmentService : IAppointmentService
    {
        private readonly ApplicationDbContext _context;

        public AppointmentService(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<Appointment>> ListAppointmentsAsync(Guid? serviceId = null)
        {
            var entities = await _context.Appointments
                .Include(x => x.User)
                .Include(x => x.Service)
                .ToListAsync();

            if (serviceId != null)
            {
                entities = entities.Where(x => x.ServiceId == serviceId).ToList();
            }

            var appointments = new List<Appointment>();
            foreach (var entity in entities)
            {
                var appointment = Conversion.ConvertAppointment(entity);

                appointment.Service = SERVICES_IMPL_SERVICES.Conversion.ConvertService(entity.Service);
                appointment.User = SERVICES_IMPL_USERS.Conversion.ConvertUser(entity.User);

                appointments.Add(appointment);
            }

            return appointments;
        }   

        public async Task<Appointment> GetAppointmentAsync(Guid id)
        {
            //TODO: Include Service, User
            var entity = await _context.Appointments.FirstOrDefaultAsync(x => x.Id == id);

            if (entity is null)
            {
                throw new ObjectNotFoundException($"Could not find an appointment with ID {id}.");
            }

            var appointment = Conversion.ConvertAppointment(entity);

            appointment.Service = SERVICES_IMPL_SERVICES.Conversion.ConvertService(entity.Service);
            appointment.User = SERVICES_IMPL_USERS.Conversion.ConvertUser(entity.User);

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
