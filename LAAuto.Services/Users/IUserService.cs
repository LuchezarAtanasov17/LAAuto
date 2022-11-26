namespace LAAuto.Services.Users
{
    public interface IUserService
    {
        Task<List<User>> ListUsersAsync();

        Task<User> GetUserAsync(Guid id);

        Task DeleteUserAsync(Guid id);
    }
}
