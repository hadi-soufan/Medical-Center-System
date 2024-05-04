namespace Domain.Entities
{
    public class PatientPhoto
    {
        public string Id { get; set; }
        public string Url { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
