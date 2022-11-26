﻿namespace LAAuto.Services.Services
{
    public class CreateServiceRequest
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public string Location { get; set; } = null!;

        public TimeOnly OpenTime { get; set; }

        public TimeOnly CloseTime { get; set; }
    }
}
