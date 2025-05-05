using System;
using System.ComponentModel.DataAnnotations;

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

    [Required, Range(1, 5)]
    public int Rooms { get; set; }

    [Required, EmailAddress]
    public string Email { get; set; } = default!;
}
