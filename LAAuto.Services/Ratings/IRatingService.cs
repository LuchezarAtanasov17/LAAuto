namespace LAAuto.Services.Ratings
{
    public interface IRatingService
    {
        Task<List<Rating>> ListRatingsAsync(Filter? filter = null);

        Task UpdateRatingAsync(Guid userId, Guid serviceId, UpdateRatingRequest request);
    }
}
