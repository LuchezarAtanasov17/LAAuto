using LAAuto.Services.Owners;

namespace LAAuto.Services.Services
{
    public class Service
    {
        public Guid Id { get; set; }

        public Guid OwnerId { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public string Location { get; set; } = null!;

        public TimeOnly OpenTime { get; set; }

        public TimeOnly CloseTime { get; set; }

        public double AverageRating { get; set; }

        public Owner Owner { get; set; }
    }
}
