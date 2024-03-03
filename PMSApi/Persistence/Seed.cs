 using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Persistence
{
    public class Seed
    {
        public static async Task SeedData(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var users = new List<AppUser>
                {
                        new AppUser
                    {
                        Email = "hatan@example.com",
                        UserName = "hatan",
                        DisplayName = "Hatan",
                        FatherName = "Michael",
                        MotherName = "Jessica",
                        DateOfBirth = new DateTime(1990, 1, 15),
                        Nationality = "American",
                        Education = "Bachelor's degree",
                        Gender = "M",
                        MaritalStatus = "Single",
                        BloodType = "O+",
                        Address = "123 Main St",
                        City = "New York",
                        ZipCode = 10001,
                        State = "NY",
                        Occupation = "Software Engineer",
                        InsuranceId = 123456789,
                        Role = "Admin",
                        Rank = 1,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now,
                        IsCancelled = false
                    },
                    new AppUser
                    {
                        Email = "janesmith@example.com",
                        UserName = "janesmith",
                        DisplayName = "Jane Smith",
                        FatherName = "David",
                        MotherName = "Jennifer",
                        DateOfBirth = new DateTime(1985, 5, 20),
                        Nationality = "Canadian",
                        Education = "Master's degree",
                        Gender = "F",
                        MaritalStatus = "Married",
                        BloodType = "AB-",
                        Address = "456 Oak St",
                        City = "Toronto",
                        ZipCode = 12345,
                        State = "ON",
                        Occupation = "Doctor",
                        InsuranceId = 987654321,
                        Role = "Patient",
                        Rank = 2,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now,
                        IsCancelled = false
                    }
                };

                foreach(var user in users)
                {
                    await userManager.CreateAsync(user, "Pa$$w0rd");
                }
            }

            if (context.Appointments.Any()) return;
            if (context.MedicalHistories.Any()) return;

            //var appointments = new List<Appointment>
            //{
            //    new Appointment
            //    {
            //        AppointmentId = Guid.NewGuid(),
            //        AppointmentDate = DateTime.UtcNow.AddMonths(1).ToUniversalTime(),
            //        AppointmentStatus = "Confirmed",
            //        AppointmentType = "OnSite",
            //        Notes = "Sample notes in English",
            //        CreatedAt = DateTime.Now.ToUniversalTime(),
            //        UpdatedAt = DateTime.Now.ToUniversalTime(),
            //        IsCancelled = false
            //    },

            //    new Appointment
            //    {
            //        AppointmentId = Guid.NewGuid(),
            //        AppointmentDate = DateTime.UtcNow.AddDays(10).ToUniversalTime(),
            //        AppointmentStatus = "Confirmed",
            //        AppointmentType = "OnSite",
            //        Notes = "Sample notes in English",
            //        CreatedAt = DateTime.Now.ToUniversalTime(),
            //        UpdatedAt = DateTime.Now.ToUniversalTime(),
            //        IsCancelled = false
            //    },

            //    new Appointment
            //    {
            //        AppointmentId = Guid.NewGuid(),
            //        AppointmentDate = DateTime.UtcNow.AddDays(4).ToUniversalTime(),
            //        AppointmentStatus = "Confirmed",
            //        AppointmentType = "OnSite",
            //        Notes = "Sample notes in English",
            //        CreatedAt = DateTime.Now.ToUniversalTime(),
            //        UpdatedAt = DateTime.Now.ToUniversalTime(),
            //        IsCancelled = false
            //    }
            //};

            //var medicalHistories = new List<MedicalHistory>
            //{
            //    new MedicalHistory
            //    {
            //        MedicalHistoryId = Guid.NewGuid(),
            //        Height = 170,
            //        Weight = 70,
            //        MedicalProblems = "None",
            //        MentalHealthProblems = "None",
            //        Allergics = "None",
            //        SugreriesHistory = "None",
            //        Vaccines = "COVID-19",
            //        Diagnosis = "Healthy",
            //        TestsPerformed = "Blood test",
            //        TreatmenPlans = "Regular exercise",
            //        FamilyMedicalHistory = "None",
            //        CreatedAt = DateTime.Now.ToUniversalTime(),
            //        UpdatedAt = DateTime.Now.ToUniversalTime(),
            //        IsDeleted = false,
            //        PatientId = "9e7bd37b-9350-4050-886d-0bc18b2d8c46",
            //    },
            //    new MedicalHistory
            //    {
            //        MedicalHistoryId = Guid.NewGuid(),
            //        Height = 170,
            //        Weight = 70,
            //        MedicalProblems = "None",
            //        MentalHealthProblems = "None",
            //        Allergics = "None",
            //        SugreriesHistory = "None",
            //        Vaccines = "COVID-19",
            //        Diagnosis = "Healthy",
            //        TestsPerformed = "Blood test",
            //        TreatmenPlans = "Regular exercise",
            //        FamilyMedicalHistory = "None",
            //        CreatedAt = DateTime.Now.ToUniversalTime(),
            //        UpdatedAt = DateTime.Now.ToUniversalTime(),
            //        IsDeleted = false
            //    },
            //    new MedicalHistory
            //    {
            //        MedicalHistoryId = Guid.NewGuid(),
            //        Height = 170,
            //        Weight = 70,
            //        MedicalProblems = "None",
            //        MentalHealthProblems = "None",
            //        Allergics = "None",
            //        SugreriesHistory = "None",
            //        Vaccines = "COVID-19",
            //        Diagnosis = "Healthy",
            //        TestsPerformed = "Blood test",
            //        TreatmenPlans = "Regular exercise",
            //        FamilyMedicalHistory = "None",
            //        CreatedAt = DateTime.Now.ToUniversalTime(),
            //        UpdatedAt = DateTime.Now.ToUniversalTime(),
            //        IsDeleted = false
            //    }
            //};

            //await context.Appointments.AddRangeAsync(appointments);
            //await context.MedicalHistories.AddRangeAsync(medicalHistories);
            await context.SaveChangesAsync();
        }
    }
}
