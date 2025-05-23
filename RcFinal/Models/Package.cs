// Models/Package.cs
using System.ComponentModel.DataAnnotations;

namespace RcFinal.Models;

public class Package
{
    [Required]
    // PackageId is a string because of InputSelect in Book.razor
    public string? PackageId { get; set; }
    [Required, MaxLength(100)]
    public string Name { get; set; } = default!;
    [Range(0, double.MaxValue)]
    public decimal PricePerNightPerGuest { get; set; }
}
