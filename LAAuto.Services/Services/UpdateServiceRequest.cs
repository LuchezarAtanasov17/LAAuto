namespace LAAuto.Services.Services
{
    public class UpdateServiceRequest
    {
        public Guid OwnerId { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public string Location { get; set; } = null!;

        public TimeOnly OpenTime { get; set; }

        public TimeOnly CloseTime { get; set; }
    }
}
