namespace LAAuto.Services.Appointments
{
    public interface IAppointmentService
    {
        Task<List<Appointment>> ListAppointmentsAsync(Guid? serviceId = null, Guid? userId = null);

        Task<Appointment> GetAppointmentAsync(Guid id);

        Task CreateAppointmentAsync(CreateAppointmentRequest request);

        Task UpdateAppointmentAsync(Guid id, UpdateAppointmentRequest request);

        Task DeleteAppointmentAsync(Guid id);
    }
}
