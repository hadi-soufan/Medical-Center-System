using Domain;
using Domain.Enums;

namespace Persistence
{
    public class Seed
    {
        public static async Task SeedData(ApplicationDbContext context)
        {
            if (context.Appointments.Any()) return;
            if (context.MedicalHistories.Any()) return;

            var appointments = new List<Appointment>
            {
                new Appointment
                {
                    AppointmentId = Guid.NewGuid(),
                    AppointmentDate = DateTime.UtcNow.AddMonths(1).ToUniversalTime(),
                    AppointmentStatus = Enums.AppointmentStatus.Confirmed,
                    AppointmentType = Enums.AppointmentType.OnSite,
                    Notes = "Sample notes in English",
                    CreatedAt = DateTime.Now.ToUniversalTime(),
                    UpdatedAt = DateTime.Now.ToUniversalTime(),
                    IsCancelled = false
                },

                new Appointment
                {
                    AppointmentId = Guid.NewGuid(),
                    AppointmentDate = DateTime.UtcNow.AddDays(10).ToUniversalTime(),
                    AppointmentStatus = Enums.AppointmentStatus.Confirmed,
                    AppointmentType = Enums.AppointmentType.OnSite,
                    Notes = "Sample notes in English",
                    CreatedAt = DateTime.Now.ToUniversalTime(),
                    UpdatedAt = DateTime.Now.ToUniversalTime(),
                    IsCancelled = false
                },

                new Appointment
                {
                    AppointmentId = Guid.NewGuid(),
                    AppointmentDate = DateTime.UtcNow.AddDays(4).ToUniversalTime(),
                    AppointmentStatus = Enums.AppointmentStatus.Confirmed,
                    AppointmentType = Enums.AppointmentType.OnSite,
                    Notes = "Sample notes in English",
                    CreatedAt = DateTime.Now.ToUniversalTime(),
                    UpdatedAt = DateTime.Now.ToUniversalTime(),
                    IsCancelled = false
                }
            };

            var medicalHistories = new List<MedicalHistory>
            {
                new MedicalHistory
                {
                    MedicalHistoryId = Guid.NewGuid(),
                    Height = 170,
                    Weight = 70,
                    MedicalProblems = "None",
                    MentalHealthProblems = "None",
                    Allergics = "None",
                    SugreriesHistory = "None",
                    Vaccines = "COVID-19",
                    Diagnosis = "Healthy",
                    TestsPerformed = "Blood test",
                    TreatmenPlans = "Regular exercise",
                    FamilyMedicalHistory = "None",
                    CreatedAt = DateTime.Now.ToUniversalTime(),
                    UpdatedAt = DateTime.Now.ToUniversalTime(),
                    IsDeleted = false
                },
                new MedicalHistory
                {
                    MedicalHistoryId = Guid.NewGuid(),
                    Height = 170,
                    Weight = 70,
                    MedicalProblems = "None",
                    MentalHealthProblems = "None",
                    Allergics = "None",
                    SugreriesHistory = "None",
                    Vaccines = "COVID-19",
                    Diagnosis = "Healthy",
                    TestsPerformed = "Blood test",
                    TreatmenPlans = "Regular exercise",
                    FamilyMedicalHistory = "None",
                    CreatedAt = DateTime.Now.ToUniversalTime(),
                    UpdatedAt = DateTime.Now.ToUniversalTime(),
                    IsDeleted = false
                },
                new MedicalHistory
                {
                    MedicalHistoryId = Guid.NewGuid(),
                    Height = 170,
                    Weight = 70,
                    MedicalProblems = "None",
                    MentalHealthProblems = "None",
                    Allergics = "None",
                    SugreriesHistory = "None",
                    Vaccines = "COVID-19",
                    Diagnosis = "Healthy",
                    TestsPerformed = "Blood test",
                    TreatmenPlans = "Regular exercise",
                    FamilyMedicalHistory = "None",
                    CreatedAt = DateTime.Now.ToUniversalTime(),
                    UpdatedAt = DateTime.Now.ToUniversalTime(),
                    IsDeleted = false
                }
            };

            await context.Appointments.AddRangeAsync(appointments);
            await context.MedicalHistories.AddRangeAsync(medicalHistories);
            await context.SaveChangesAsync();
        }
    }
}
