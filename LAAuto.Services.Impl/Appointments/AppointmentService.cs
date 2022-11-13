using LAAuto.Entities.Data;
using LAAuto.Services.Appointments;
using Microsoft.EntityFrameworkCore;
using ENTITIES = LAAuto.Entities.Models;

namespace LAAuto.Services.Impl.Appointments
{
    public class AppointmentService : IAppointmentService
    {
        private readonly ApplicationDbContext _context;

        public AppointmentService(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<Appointment>> ListAppointmentsAsync()
        {
            List<ENTITIES.Appointment> entities = await _context.Appointments.ToListAsync();

            var appointments = entities
                .Select(Conversion.ConvertAppointment)
                .ToList();

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

            entity.ClientId = request.ClientId;
            entity.CategoryId = request.CategoryId;
            entity.ClientId = request.ClientId;
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
