using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    /// <summary>
    /// Represents nurse entity.
    /// </summary>
    public class Nurse
    {
        public Guid NurseId { get; set; }
        public int NurseLicenseId { get; set; }
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public AppUser User { get; set; }

        public DateOnly CreatedAt { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);
        public DateOnly UpdatedAt { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
