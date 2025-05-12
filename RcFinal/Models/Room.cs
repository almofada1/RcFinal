using System.ComponentModel.DataAnnotations;

namespace RcFinal.Models
{
    public class Room
    {
        [Key]
        public int RoomId { get; set; }
        public int Capacity { get; set; }
    }
}
