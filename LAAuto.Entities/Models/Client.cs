using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LAAuto.Entities.Models
{
    public class Client : IdentityUser<Guid>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [StringLength(30)]
        public string FirstName { get; set; } = null!;

        [Required]
        [StringLength(30)]
        public string LastName { get; set; } = null!;

        public ICollection<Appointment> Appointments { get; set; } = new HashSet<Appointment>();
    }
}
