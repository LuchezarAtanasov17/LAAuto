using LAAuto.Services.Users;
using LAAuto.Services.Services;

namespace LAAuto.Services.Ratings
{
    public class Rating
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public Guid ServiceId { get; set; }

        public int Value { get; set; }

        public User User { get; set; }

        public Service Service { get; set; }
    }
}
