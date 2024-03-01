using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    /// <summary>
    /// Represents the user entity.
    /// </summary>
    public class AppUser : IdentityUser
    {
        [StringLength(150)]
        public string DisplayName { get; set; }

        [StringLength(150)]
        public string FatherName { get; set; }

        [StringLength(225)]
        public string MotherName { get; set; }
        public DateOnly DateOfBirth { get; set; }

        [StringLength(150)]
        public string Nationality { get; set; }

        [StringLength(150)]
        public string Education { get; set; }

        [StringLength(6)]
        public string Gender { get; set; }

        [StringLength(50)]
        public string MaritalStatus { get; set; }

        [StringLength(10)]
        public string BloodType { get; set; }

        [StringLength(255)]
        public string Address { get; set; }

        [StringLength(255)]
        public string City { get; set; }
        public int ZipCode { get; set; }

        [StringLength(255)]
        public string State { get; set; }

        [StringLength(255)]
        public string Occupation { get; set; }
        public int InsuranceId { get; set; }

        [StringLength(255)]
        public string Role { get; set; }
        public int Rank { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; }
        public bool IsCancelled { get; set; }

    }
}
