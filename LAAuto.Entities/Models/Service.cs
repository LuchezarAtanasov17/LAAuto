using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LAAuto.Entities.Models
{
    public class Service
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; } = null!;

        [StringLength(300)]
        public string? Description { get; set; }

        [Required]
        [StringLength(80)]
        public string Location { get; set; } = null!;

        [Required]
        public TimeOnly OpenTime { get; set; }

        [Required]
        public TimeOnly CloseTime { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        public byte[]? Image { get; set; }
        public ICollection<Appointment> Appointments { get; set; } = new HashSet<Appointment>();

        public ICollection<Rating> Ratings { get; set; } = new HashSet<Rating>();

        public ICollection<CategoryService> CategoryServices { get; set; } = new HashSet<CategoryService>();
    }
}
