using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Center
    {
        public Guid CenterId { get; set; }
        public string CenterLocation { get; set; }
        public string CenterName { get; set; }
        public DateOnly CreatedAt { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);
        public bool IsDeleted { get; set; } = false;
        public ICollection<Building> Buildings { get; set; } = new List<Building>();
    }
}
