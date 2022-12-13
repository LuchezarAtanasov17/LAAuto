﻿using LAAuto.Web.Models.Categories;
using LAAuto.Web.Models.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LAAuto.Web.Models.Appointments
{
    public class CreateAppointmentRequest
    {
        public Guid ServiceId { get; set; }

        public Guid UserId { get; set; }

        public Guid CategoryId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int StartDateHour { get; set; }

        public string? Description { get; set; }

        public ServiceViewModel? Service { get; set; }

        public CategoryViewModel? Category { get; set; }
    }
}
