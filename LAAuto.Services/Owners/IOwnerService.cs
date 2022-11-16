namespace LAAuto.Services.Owners
{
    public interface IOwnerService
    {
        Task<List<Owner>> ListOwnersAsync();

        Task<Owner> GetOwnerAsync(Guid id);

        Task UpdateOwnerAsync(Guid id, UpdateOwnerRequest request);

        Task CreateOwnerAsync(CreateOwnerRequest request);

        Task DeleteOwnerAsync(Guid id);
    }
}
