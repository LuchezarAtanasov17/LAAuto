using LAAuto.Services.Categories;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace LAAuto.Services.Services
{
    public class CreateServiceRequest
    {
        public Guid UserId { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
        public string Name { get; set; } = null!;

        [StringLength(300)]
        public string? Description { get; set; }

        [Required]
        [StringLength(80, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 10)]
        public string Location { get; set; } = null!;

        public TimeOnly OpenTime { get; set; }

        public TimeOnly CloseTime { get; set; }

        public byte[]? Image { get; set; }

        public List<Category> Categories { get; set; }

    }
}
