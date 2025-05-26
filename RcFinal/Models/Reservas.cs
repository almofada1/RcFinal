// Models/Reservas.cs
using RcFinal.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

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

    [Required, EmailAddress]
    public string Email { get; set; } = default!;

    [Required]
    public int RoomId { get; set; }
    [ForeignKey(nameof(RoomId))]
    public Quartos Quartos { get; set; } = default!;
    public string? PackageId { get; set; }
    public Package? Package { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalCost { get; set; }
    public int EstadoId { get; set; }
    public Estado Estado { get; set; } = null!;
}
