namespace LAAuto.Services.Services
{
    public interface IServiceService
    {
        Task<List<Service>> ListServicesAsync();

        Task<Service> GetServiceAsync(Guid id);

        Task CreateServiceAsync(CreateServiceRequest request);

        Task UpdateServiceAsync(Guid id, UpdateServiceRequest request);

        Task DeleteServiceAsync(Guid id);

        Task<bool> ExistById(Guid id);
    }
}
