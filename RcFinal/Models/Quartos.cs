using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RcFinal.Models
{
    public class Quartos
    {
        [Key]
        [Column("RoomId")]        // ensure EF always thinks column is already RoomId
        public int RoomId { get; set; }
        public int Capacidade { get; set; }
    }
}
