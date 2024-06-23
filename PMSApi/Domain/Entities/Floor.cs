using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Floor
    {
        public Guid FloorId { get; set; }
        public int FloorNumber { get; set; }
        public int RoomCount { get; set; }
        public DateOnly CreatedAt { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);
        public bool IsDeleted { get; set; } = false;
        public Guid DepartmentId { get; set; }
        public Department Department { get; set; }

        public ICollection<Room> Rooms { get; set; } = new List<Room>();
    }
}
