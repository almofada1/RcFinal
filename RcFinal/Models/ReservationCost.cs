// Models/ReservationCost.cs
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RcFinal.Models
{
    public class ReservationCost
    {
        [Key]
        public int ReservationCostId { get; set; }

        // FK back to Reservas.Id
        [Required]
        public int ReservaId { get; set; }
        [ForeignKey(nameof(ReservaId))]
        public Reservas? Reserva { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Cost { get; set; }

        public DateTime RecordedAt { get; set; }
    }
}
