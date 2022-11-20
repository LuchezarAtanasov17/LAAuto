using LAAuto.Entities.Models;

namespace LAAuto.Web.Models.Ratings
{
    public class UpdateRatingRequest
    {
        public Guid UserId { get; set; }

        public Guid ServiceId { get; set; }

        public int Value { get; set; }
    }
}
