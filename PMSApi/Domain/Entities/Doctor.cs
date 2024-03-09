using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    /// <summary>
    /// Represents doctor entity.
    /// </summary>
    public class Doctor
    {
        public Guid DoctorId { get; set; }
        public int DoctorLicenseId { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]  
        public AppUser User { get; set; }
        public DateOnly CreatedAt { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);
        public DateOnly UpdatedAt { get; set; }
        public bool IsDeleted { get; set; } = false;

        public ICollection<Appointment> Appointments { get; set; }

    }
}
