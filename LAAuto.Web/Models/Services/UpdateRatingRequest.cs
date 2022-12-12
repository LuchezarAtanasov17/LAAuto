namespace LAAuto.Web.Models.Services
{
    public class UpdateRatingRequest
    {
        public Guid ServiceId { get; set; }
        public Guid UserId { get; set; }
        public int Value { get; set; }
        public double AverageRating { get; set; }
    }
}
