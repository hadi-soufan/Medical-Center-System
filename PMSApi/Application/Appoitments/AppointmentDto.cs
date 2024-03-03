using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Appoitments
{
    public class AppointmentDto
    {
        public Guid AppointmentId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string AppointmentStatus { get; set; }
        public string AppointmentType { get; set; }
        public string Notes { get; set; }
        public string PatientUsername { get; set; }
        public string DoctorUsername { get; set; }
    }
}
