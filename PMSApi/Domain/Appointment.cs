using System.ComponentModel.DataAnnotations;
using static Domain.Enums.Enums;

namespace Domain
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
        public AppointmentStatus AppointmentStatus { get; set; }

        [Display(Name = "Appointment Type")]
        public AppointmentType AppointmentType { get; set; }

        [Display(Name = "Appointment Date")]
        [StringLength(255)]
        public string Notes { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; }
        public bool IsCancelled { get; set; }
    }
}
