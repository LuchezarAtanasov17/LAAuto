namespace LAAuto.Services.Services
{
    public interface IServiceService
    {
        Task<List<Service>> ListServicesAsync(Guid? userId = null);

        Task<Service> GetServiceAsync(Guid id);

        Task CreateServiceAsync(CreateServiceRequest request);

        Task UpdateServiceAsync(Guid id, UpdateServiceRequest request);

        Task UpdateServiceRatingAsync(UpdateRatingRequest request);

        Task DeleteServiceAsync(Guid id);

        Task CancelAppointmentAsync(Guid id);
    }
}
