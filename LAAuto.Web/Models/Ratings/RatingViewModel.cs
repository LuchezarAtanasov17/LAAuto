using LAAuto.Web.Models.Services;
using LAAuto.Web.Models.Users;

namespace LAAuto.Web.Models.Ratings
{
    public class RatingViewModel
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public Guid ServiceId { get; set; }

        public int Value { get; set; }

        public UserViewModel User { get; set; }

        public ServiceViewModel Service { get; set; }
    }
}
