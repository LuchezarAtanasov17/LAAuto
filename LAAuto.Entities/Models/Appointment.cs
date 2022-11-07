using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LAAuto.Entities.Models
{
    public class Appointment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public Guid ServiceId { get; set; }
        
        [Required]
        public Guid ClientId { get; set; }
        
        [Required]
        public Guid CategoryId { get; set; }

        [Required]
        public DateTime StartDate { get; set; }
        
        [Required]
        public DateTime EndDate { get; set; }

        [StringLength(300)]
        public string? Description { get; set; }

        [Required]
        [ForeignKey(nameof(ServiceId))]
        public Service Service { get; set; }

        [Required]
        [ForeignKey(nameof(ClientId))]
        public Client Client { get; set; }

        [Required]
        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; }

    }
}
