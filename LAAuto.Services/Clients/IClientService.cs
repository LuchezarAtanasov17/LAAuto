namespace LAAuto.Services.Clients
{
    public interface IClientService
    {
        Task<List<Client>> ListCategoriesAsync();

        Task<Client> GetCategoryAsync(Guid id);

        Task UpdateClientService(Guid id, UpdateClientRequest request);

        Task CreateClientService(CreateClientRequest request);
    }
}
