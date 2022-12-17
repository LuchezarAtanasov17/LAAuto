namespace LAAuto.Services.Appointments
{
    /// <summary>
    /// Provides access to appointments.
    /// </summary>
    public interface IAppointmentService
    {
        /// <summary>
        /// Lists the appointments.
        /// </summary>
        /// <param name="serviceId">the service ID</param>
        /// <param name="userId">the user ID</param>
        /// <returns>collection of appointments</returns>
        Task<List<Appointment>> ListAppointmentsAsync(Guid? serviceId = null, Guid? userId = null);

        /// <summary>
        /// Gets an appointment with specified ID.
        /// </summary>
        /// <param name="id">the appointment ID</param>
        /// <returns>an appointment</returns>
        Task<Appointment> GetAppointmentAsync(Guid id);

        /// <summary>
        /// Creates an appointment.
        /// </summary>
        /// <param name="request">the request for creating an appointment</param>
        Task CreateAppointmentAsync(CreateAppointmentRequest request);

        /// <summary>
        /// Updates a specified appointment.
        /// </summary>
        /// <param name="id">the appointment ID</param>
        /// <param name="request">the request for creating an appointment</param>
        Task UpdateAppointmentAsync(Guid id, UpdateAppointmentRequest request);

        /// <summary>
        /// Delete specified appointment.
        /// </summary>
        /// <param name="id">the appointment ID</param>
        Task DeleteAppointmentAsync(Guid id);
    }
}
