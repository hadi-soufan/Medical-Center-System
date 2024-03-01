using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    /// <summary>
    /// Represents medical history information for a patient.
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
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; }
        public bool IsCancelled { get; set; }
    }
}
