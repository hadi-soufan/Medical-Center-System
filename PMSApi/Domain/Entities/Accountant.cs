using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    /// <summary>
    /// Represents accountant entity.
    /// </summary>
    public class Accountant
    {
        public Guid AccountantId { get; set; }
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public AppUser User { get; set; }

        public DateOnly CreatedAt { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);
        public DateOnly  UpdatedAt { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}
