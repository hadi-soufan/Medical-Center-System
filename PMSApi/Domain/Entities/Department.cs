using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Department
    {
        public Guid DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentPhone { get; set; }
        public string DepartmentEmail { get; set; }
        public string DepartmentFax { get; set; }
        public DateOnly CreatedAt { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);
        public bool IsDeleted { get; set; } = false;
        public Guid BuildingId { get; set; }
        public Building Building { get; set; }

        public ICollection<Floor> Floors { get; set; } = new List<Floor>();
    }
}
