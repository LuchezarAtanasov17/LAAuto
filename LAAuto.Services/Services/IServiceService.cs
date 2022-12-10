namespace LAAuto.Services.Services
{
    public interface IServiceService
    {
        Task<List<Service>> ListServicesAsync(string? categoryFilter = null);

        Task<List<Service>> ListMyServicesAsync(Guid userId);

        Task<Service> GetServiceAsync(Guid id);

        Task CreateServiceAsync(CreateServiceRequest request);

        Task UpdateServiceAsync(Guid id, UpdateServiceRequest request);

        Task DeleteServiceAsync(Guid id);

        Task CancelAppointmentAsync(Guid id);
    }
}
