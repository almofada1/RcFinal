using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RcFinal.Models
{
    public class Reservas
    {
        [Key]
        public int Id { get; set; }

        [Required, DataType(DataType.Date)]
        public DateTime CheckIn { get; set; }

        [Required, DataType(DataType.Date)]
        public DateTime CheckOut { get; set; }

        [Required, Range(1, 10)]
        public int Guests { get; set; }

        [Required]
        public int RoomId { get; set; }

        // ← Place ForeignKey on the navigation:
        [ForeignKey(nameof(RoomId))]
        public Quartos Quartos { get; set; } = default!;

        [Required, EmailAddress]
        public string Email { get; set; } = default!;
    }
}
