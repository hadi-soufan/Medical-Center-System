using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    /// <summary>
    /// Represents appointment entity.
    /// </summary>
    public class Appointment
    {
        [Display(Name = "Appointment Id")]
        public Guid AppointmentId { get; set; }

        [Display(Name = "Appointment Date")]
        public DateTime AppointmentDate { get; set; }

        [Display(Name = "Appointment Status")]
        [StringLength(20)]
        public string AppointmentStatus { get; set; }

        [Display(Name = "Appointment Type")]
        [StringLength(20)]
        public string AppointmentType { get; set; }

        [Display(Name = "Appointment Date")]
        [StringLength(255)]
        public string Notes { get; set; }
        public DateOnly CreatedAt { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);
        public DateOnly UpdatedAt { get; set; }
        public bool IsCancelled { get; set; } = false;
        public Guid PatientId { get; set; }

        [ForeignKey("PatientId")]
        public Patient Patient{ get; set; }

        public Guid DoctorId { get; set; }
        [ForeignKey("DoctorId")]
        public Doctor Doctor { get; set; }
    }
}
