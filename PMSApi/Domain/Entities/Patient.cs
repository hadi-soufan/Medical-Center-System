using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class Patient
    {
        public Guid PatientId { get; set; }
       
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public AppUser User { get; set; }

        public ICollection<Appointment> Appointments { get; set; }

    }
}
