using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LAAuto.Entities.Models
{
    public class Rating
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        
        [Required]
        public Guid ClientId { get; set; }

        [Required]
        public Guid ServiceId { get; set; }

        [Required]
        public int Value { get; set; }

        [Required]
        [ForeignKey(nameof(ClientId))]
        public Client Client { get; set; }

        [Required]
        [ForeignKey(nameof(ServiceId))]
        public Service Service { get; set; }
    }
}
