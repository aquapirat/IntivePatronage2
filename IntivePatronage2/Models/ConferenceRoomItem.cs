using System.ComponentModel.DataAnnotations;

namespace IntivePatronage2.Models
{
    public class ConferenceRoomItem
    {
        public int Id { set; get; }

        [Required]
        public int RoomNumber { set; get; }

        [Required]
        public int Capacity { set; get; }
        
    }
}
