using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Room
    {
        public Guid RoomId { get; set; }
        public string RoomCoe { get; set; }
        public DateOnly CreatedAt { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);
        public bool IsDeleted { get; set; } = false;
        public Guid FloorId { get; set; }
        public Floor Floor { get; set; }
    }
}
