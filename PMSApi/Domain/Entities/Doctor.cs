using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Doctor
    {
        public Guid DoctorId { get; set; }
        public int DoctorLicenseId { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]  
        public AppUser User { get; set; }

        public ICollection<Appointment> Appointments { get; set; }

    }
}
