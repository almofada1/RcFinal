using System.ComponentModel.DataAnnotations;

namespace RcFinal.Models
{
    public class Estado
    {
        public int Id { get; set; }
        public string Nome { get; set; } = null!;

        // navigation back to reservations (optional)
        public ICollection<Reservas>? Reservas { get; set; }
    }
}