using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    /// <summary>
    /// Represents medical history information for a patient.
    /// </summary>
    public class MedicalHistory
    {
        [Display(Name = "Medical History ID")]
        public Guid MedicalHistoryId { get; set; }

        [Display(Name = "Patient Height")]
        public decimal Height { get; set; }

        [Display(Name = "Patient Weight")]
        public decimal Weight { get; set; }

        [Display(Name = "Patient Medical Problems")]
        [StringLength(255)]
        public string MedicalProblems { get; set; }

        [Display(Name = "Patient Mental Health Problems")]
        [StringLength(255)]
        public string MentalHealthProblems { get; set; }

        [Display(Name = "Patient Allergics")]
        [StringLength(255)]
        public string Allergics { get; set; }

        [Display(Name = "Patient Sugreries History")]
        [StringLength(255)]
        public string SugreriesHistory { get; set; }

        [Display(Name = "Patient Vaccines")]
        [StringLength(255)]
        public string Vaccines { get; set; }

        [Display(Name = "Patient Diagnosis")]
        [StringLength(255)]
        public string Diagnosis { get; set; }

        [Display(Name = "Patient Tests Performed")]
        [StringLength(255)]
        public string TestsPerformed { get; set; }

        [Display(Name = "Patient Treatmen Plans")]
        [StringLength(255)]
        public string TreatmenPlans { get; set; }

        [Display(Name = "Patient Family Medical History")]
        [StringLength(255)]
        public string FamilyMedicalHistory { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; }

        public bool IsDeleted { get; set; }
    }
}
