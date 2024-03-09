using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    /// <summary>
    /// Represents patient entity.
    /// </summary>
    public class Patient
    {
        public Guid PatientId { get; set; }
       
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public AppUser User { get; set; }

        public DateOnly CreatedAt { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);
        public DateOnly UpdatedAt { get; set; }
        public bool IsDeleted { get; set; } = false;

        public ICollection<Appointment> Appointments { get; set; }

    }
}
