namespace LAAuto.Services.Users
{
    /// <summary>
    /// Provides access to users.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Lists the users.
        /// </summary>
        /// <returns>collection of users</returns>
        Task<List<User>> ListUsersAsync();

        /// <summary>
        /// Gets specified user.
        /// </summary>
        /// <param name="id">user ID</param>
        /// <returns>a user</returns>
        Task<User> GetUserAsync(Guid id);

        /// <summary>
        /// Deletes specified user.
        /// </summary>
        /// <param name="id">user ID</param>
        Task DeleteUserAsync(Guid id);
    }
}
