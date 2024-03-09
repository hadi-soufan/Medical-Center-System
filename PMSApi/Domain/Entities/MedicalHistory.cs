using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    /// <summary>
    /// Represents medical history entity for a patient.
    /// </summary>
    public class MedicalHistory
    {
        public Guid MedicalHistoryId { get; set; }

        public decimal Height { get; set; }

        public decimal Weight { get; set; }

        [StringLength(255)]
        public string MedicalProblems { get; set; }

        [StringLength(255)]
        public string MentalHealthProblems { get; set; }

        [StringLength(255)]
        public string Medicines { get; set; }

        [StringLength(255)]
        public string Allergics { get; set; }

        [StringLength(255)]
        public string SugreriesHistory { get; set; }

        [StringLength(255)]
        public string Vaccines { get; set; }

        [StringLength(255)]
        public string Diagnosis { get; set; }

        [StringLength(255)]
        public string TestsPerformed { get; set; }

        [StringLength(255)]
        public string TreatmenPlans { get; set; }

        [StringLength(255)]
        public string FamilyMedicalHistory { get; set; }

        public DateOnly CreatedAt { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);
        public DateOnly UpdatedAt { get; set; }
        public bool IsDeleted { get; set; } = false;

        public Guid PatientId { get; set; }
        
        [ForeignKey("PatientId")]
        public Patient Patient { get; set; }

        public string PatientName { get; set; }
    }
}
