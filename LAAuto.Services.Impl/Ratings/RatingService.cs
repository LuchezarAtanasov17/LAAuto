using LAAuto.Entities.Data;
using LAAuto.Services.Ratings;
using Microsoft.EntityFrameworkCore;

namespace LAAuto.Services.Impl.Ratings
{
    public class RatingService : IRatingService
    {
        private readonly ApplicationDbContext _context;

        public RatingService(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<Rating>> ListRatingsAsync(Filter filter)
        {
            //TODO:
            //var entities = _context.Ratings.Where()
            throw new NotImplementedException();
        }

        public async Task UpdateRatingAsync(Guid ClientId, Guid ServiceId, UpdateRatingRequest request)
        {
            var entities = await _context.Ratings
                .Where(x => x.UserId == ClientId && x.ServiceId == ServiceId)
                .ToListAsync();

            _context.Ratings.RemoveRange(entities);
            
            await _context.SaveChangesAsync();
        }
    }
}
