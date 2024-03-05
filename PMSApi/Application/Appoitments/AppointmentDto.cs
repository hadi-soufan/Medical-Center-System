namespace Application.Appoitments
{
    /// <summary>
    /// Data transfer object for appointment information.
    /// </summary>
    public class AppointmentDto
    {
        public Guid AppointmentId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string AppointmentStatus { get; set; }
        public string AppointmentType { get; set; }
        public string Notes { get; set; }
        public string PatientUsername { get; set; }
        public string DoctorUsername { get; set; }
    }
}
