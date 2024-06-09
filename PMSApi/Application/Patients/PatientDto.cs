using Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Application.Patients
{
    /// <summary>
    /// Data transfer object for doctor information.
    /// </summary>
    public class PatientDto
    {
        public Guid PatientId { get; set; }
        [StringLength(150)]
        public string DisplayName { get; set; }

        [StringLength(150)]
        public string Username { get; set; }
        [StringLength(150)]
        public string Email { get; set; }
        [StringLength(20)]
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        [StringLength(6)]
        public string Gender { get; set; }
        [StringLength(5)]
        public string BloodType { get; set; }
        [StringLength(255)]
        public string Address { get; set; }
        [StringLength(255)]
        public string Occupation { get; set; }
        public int InsuranceId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string MedicalHistoryId { get; set; }
        public AppUser User{ get; set; }

    }
}
