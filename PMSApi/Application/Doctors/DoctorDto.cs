using System.ComponentModel.DataAnnotations;

namespace Application.Doctors
{
    public class DoctorDto
    {
        public Guid DoctorId { get; set; }

        [StringLength(150)]
        public string DisplayName { get; set; }

        [StringLength(150)]
        public string Username { get; set; }

        [StringLength(150)]
        public string Email { get; set; }

        [StringLength(150)]
        public string PhoneNumber { get; set; }

        [StringLength(150)]
        public string FatherName { get; set; }

        [StringLength(225)]
        public string MotherName { get; set; }
        public DateTime DateOfBirth { get; set; }

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

        public int DoctorLicenseId { get; set; }

    }
}
