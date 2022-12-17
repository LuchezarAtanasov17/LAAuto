namespace LAAuto.Services.Services
{
    /// <summary>
    /// Provides access to services.
    /// </summary>
    public interface IServiceService
    {
        /// <summary>
        /// Lists the services.
        /// </summary>
        /// <param name="userId">the user ID</param>
        /// <returns>collection of services</returns>
        Task<List<Service>> ListServicesAsync(Guid? userId = null);

        /// <summary>
        /// Gets a specified service.
        /// </summary>
        /// <param name="id">the service ID</param>
        /// <returns>a service</returns>
        Task<Service> GetServiceAsync(Guid id);

        /// <summary>
        /// Creates a service.
        /// </summary>
        /// <param name="request">the request for creating service</param>
        Task CreateServiceAsync(CreateServiceRequest request);

        /// <summary>
        /// Updates a specified service.
        /// </summary>
        /// <param name="id">service ID</param>
        /// <param name="request">the request for updating service</param>
        Task UpdateServiceAsync(Guid id, UpdateServiceRequest request);

        /// <summary>
        /// Updates a service rating.
        /// </summary>
        /// <param name="request">the request for updating rating</param>
        Task UpdateServiceRatingAsync(UpdateRatingRequest request);

        /// <summary>
        /// Deletes a specified service.
        /// </summary>
        /// <param name="id">service ID</param>
        Task DeleteServiceAsync(Guid id);
    }
}
