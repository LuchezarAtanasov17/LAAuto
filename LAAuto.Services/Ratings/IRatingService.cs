namespace LAAuto.Services.Ratings
{
    public interface IRatingService
    {
        Task<List<Rating>> ListRatingsAsync(Filter? filter = null);

        Task UpdateRatingAsync(Guid ClientId, Guid ServiceId, UpdateRatingRequest request);
    }
}
