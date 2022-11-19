using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LAAuto.Entities.Models
{
    public class User : IdentityUser<Guid>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [StringLength(30)]
        public string? FirstName { get; set; }

        [StringLength(30)]
        public string? LastName { get; set; }

        public ICollection<Appointment> Appointments { get; set; } = new HashSet<Appointment>();

        public ICollection<Service> Services { get; set; } = new HashSet<Service>();

        public ICollection<Rating> Ratings { get; set; } = new HashSet<Rating>();
    }
}
