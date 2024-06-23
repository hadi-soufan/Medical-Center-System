using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Building
    {
        public Guid BuildingId { get; set; }
        public string BuildingName { get; set; }
        public DateOnly CreatedAt { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);
        public bool IsDeleted { get; set; } = false;
        public Guid CenterId { get; set; }
        public Center Center { get; set; }

        public ICollection<Department> Departments { get; set; } = new List<Department>();
    }
}
