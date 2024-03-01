using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class RegisterDto
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        [Required]
        [StringLength(150)]
        public string DisplayName { get; set; }

        [Required]
        [StringLength(150)]
        public string FatherName { get; set; }

        [Required]
        [StringLength(225)]
        public string MotherName { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [StringLength(150)]
        public string Nationality { get; set; }

        [Required]
        [StringLength(150)]
        public string Education { get; set; }

        [Required]
        [StringLength(6)]
        public string Gender { get; set; }

        [Required]
        [StringLength(50)]
        public string MaritalStatus { get; set; }

        [Required]
        [StringLength(10)]
        public string BloodType { get; set; }

        [Required]
        [StringLength(255)]
        public string Address { get; set; }

        [Required]
        [StringLength(255)]
        public string City { get; set; }

        public int ZipCode { get; set; }

        [Required]
        [StringLength(255)]
        public string State { get; set; }

        [StringLength(255)]
        public string Occupation { get; set; }

        public int InsuranceId { get; set; }

        [StringLength(255)]
        public string Role { get; set; }

    }
}
