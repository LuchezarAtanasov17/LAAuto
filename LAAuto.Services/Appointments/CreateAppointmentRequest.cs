﻿using LAAuto.Services.Categories;

namespace LAAuto.Services.Appointments
{
    public class CreateAppointmentRequest
    {
        public Guid ServiceId { get; set; }

        public Guid UserId { get; set; }

        public Guid CategoryId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string? Description { get; set; }

        public Category Category { get; set; }
    }
}
