using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RcFinal.Models;
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

    // NEW: foreign key to Room
    [Required]
    public int RoomId { get; set; }

    [ForeignKey(nameof(RoomId))]
    public Room? Room { get; set; }

    [Required, EmailAddress]
    public string Email { get; set; } = default!;
}
