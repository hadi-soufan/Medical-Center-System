using Application.Accountants;
using Application.Appoitments;
using Application.Doctors;
using Application.MedicalHistoreis;
using Application.Nurses;
using Application.Patients;
using Application.Receptionist;
using Application.Staffs;
using AutoMapper;
using Domain.Entities;

/// <summary>
/// Represents a mapping profile for AutoMapper.
/// </summary>
public class MappingProfiles : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MappingProfiles"/> class.
    /// </summary>
    public MappingProfiles()
    {
        CreateMap<Appointment, Appointment>();
        CreateMap<MedicalHistory, MedicalHistory>();

        CreateMap<PatientDto, Patient>();

        CreateMap<Receptionist, ReceptionistDto>()
                    .ForMember(dest => dest.ReceptionistId, opt => opt.MapFrom(src => src.ReceptionistId))
                    .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.User.DisplayName))
                    .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.User.UserName))
                    .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
                    .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.User.PhoneNumber))
                    .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.User.DateOfBirth))
                    .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.User.Gender))
                    .ForMember(dest => dest.BloodType, opt => opt.MapFrom(src => src.User.BloodType))
                    .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.User.Address))
                    .ForMember(dest => dest.Occupation, opt => opt.MapFrom(src => src.User.Occupation))
                    .ForMember(dest => dest.InsuranceId, opt => opt.MapFrom(src => src.User.InsuranceId));


        CreateMap<Accountant, AccoutantDto>()
               .ForMember(dest => dest.AccountantId, opt => opt.MapFrom(src => src.AccountantId))
               .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.User.DisplayName))
               .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
               .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.User.UserName))
               .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.User.PhoneNumber))
               .ForMember(dest => dest.FatherName, opt => opt.MapFrom(src => src.User.FatherName))
               .ForMember(dest => dest.MotherName, opt => opt.MapFrom(src => src.User.MotherName))
               .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.User.DateOfBirth))
               .ForMember(dest => dest.Nationality, opt => opt.MapFrom(src => src.User.Nationality))
               .ForMember(dest => dest.Education, opt => opt.MapFrom(src => src.User.Education))
               .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.User.Gender))
               .ForMember(dest => dest.MaritalStatus, opt => opt.MapFrom(src => src.User.MaritalStatus))
               .ForMember(dest => dest.BloodType, opt => opt.MapFrom(src => src.User.BloodType))
               .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.User.Address))
               .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.User.City))
               .ForMember(dest => dest.ZipCode, opt => opt.MapFrom(src => src.User.ZipCode))
               .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.User.State))
               .ForMember(dest => dest.Occupation, opt => opt.MapFrom(src => src.User.Occupation))
               .ForMember(dest => dest.InsuranceId, opt => opt.MapFrom(src => src.User.InsuranceId));

        CreateMap<Appointment, AppointmentDto>()
            .ForMember(dest => dest.PatientUsername, opt => opt.MapFrom(src => src.Patient.User.UserName))
            .ForMember(dest => dest.DoctorUsername, opt => opt.MapFrom(src => src.Doctor.User.UserName));

        CreateMap<AppointmentDto, Appointment>();


        CreateMap<Doctor, DoctorDto>()
                   .ForMember(dest => dest.DoctorId, opt => opt.MapFrom(src => src.DoctorId))
                   .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.User.DisplayName))
                   .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
                   .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.User.UserName))
                   .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.User.PhoneNumber))
                   .ForMember(dest => dest.FatherName, opt => opt.MapFrom(src => src.User.FatherName))
                   .ForMember(dest => dest.MotherName, opt => opt.MapFrom(src => src.User.MotherName))
                   .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.User.DateOfBirth))
                   .ForMember(dest => dest.Nationality, opt => opt.MapFrom(src => src.User.Nationality))
                   .ForMember(dest => dest.Education, opt => opt.MapFrom(src => src.User.Education))
                   .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.User.Gender))
                   .ForMember(dest => dest.MaritalStatus, opt => opt.MapFrom(src => src.User.MaritalStatus))
                   .ForMember(dest => dest.BloodType, opt => opt.MapFrom(src => src.User.BloodType))
                   .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.User.Address))
                   .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.User.City))
                   .ForMember(dest => dest.ZipCode, opt => opt.MapFrom(src => src.User.ZipCode))
                   .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.User.State))
                   .ForMember(dest => dest.Occupation, opt => opt.MapFrom(src => src.User.Occupation))
                   .ForMember(dest => dest.InsuranceId, opt => opt.MapFrom(src => src.User.InsuranceId))
                   .ForMember(dest => dest.DoctorLicenseId, opt => opt.MapFrom(src => src.DoctorLicenseId));

        CreateMap<MedicalHistory, MedicalHistoryDto>()
                    .ForMember(dest => dest.PatientName, opt => opt.MapFrom(src => src.Patient.User.DisplayName));


        CreateMap<Nurse, NurseDto>()
                    .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.User.DisplayName))
                    .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
                    .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.User.UserName))
                    .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.User.PhoneNumber))
                    .ForMember(dest => dest.FatherName, opt => opt.MapFrom(src => src.User.FatherName))
                    .ForMember(dest => dest.MotherName, opt => opt.MapFrom(src => src.User.MotherName))
                    .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.User.DateOfBirth))
                    .ForMember(dest => dest.Nationality, opt => opt.MapFrom(src => src.User.Nationality))
                    .ForMember(dest => dest.Education, opt => opt.MapFrom(src => src.User.Education))
                    .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.User.Gender))
                    .ForMember(dest => dest.MaritalStatus, opt => opt.MapFrom(src => src.User.MaritalStatus))
                    .ForMember(dest => dest.BloodType, opt => opt.MapFrom(src => src.User.BloodType))
                    .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.User.Address))
                    .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.User.City))
                    .ForMember(dest => dest.ZipCode, opt => opt.MapFrom(src => src.User.ZipCode))
                    .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.User.State))
                    .ForMember(dest => dest.Occupation, opt => opt.MapFrom(src => src.User.Occupation))
                    .ForMember(dest => dest.InsuranceId, opt => opt.MapFrom(src => src.User.InsuranceId))
                    .ForMember(dest => dest.NurseLicenseId, opt => opt.MapFrom(src => src.NurseLicenseId));

        CreateMap<Nurse, NurseDto>()
                    .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.User.DisplayName))
                    .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.User.UserName))
                    .ForMember(dest => dest.FatherName, opt => opt.MapFrom(src => src.User.FatherName))
                    .ForMember(dest => dest.MotherName, opt => opt.MapFrom(src => src.User.MotherName))
                    .ForMember(dest => dest.Nationality, opt => opt.MapFrom(src => src.User.Nationality))
                    .ForMember(dest => dest.Education, opt => opt.MapFrom(src => src.User.Education))
                    .ForMember(dest => dest.MaritalStatus, opt => opt.MapFrom(src => src.User.MaritalStatus))
                    .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.User.City))
                    .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.User.State))
                    .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
                    .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.User.PhoneNumber))
                    .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.User.DateOfBirth))
                    .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.User.Gender))
                    .ForMember(dest => dest.BloodType, opt => opt.MapFrom(src => src.User.BloodType))
                    .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.User.Address))
                    .ForMember(dest => dest.Occupation, opt => opt.MapFrom(src => src.User.Occupation))
                    .ForMember(dest => dest.InsuranceId, opt => opt.MapFrom(src => src.User.InsuranceId))
                    .ForMember(dest => dest.NurseLicenseId, opt => opt.MapFrom(src => src.NurseLicenseId));

        CreateMap<Patient, PatientDto>()
                .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.User.DisplayName))
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.User.UserName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.User.PhoneNumber))
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.User.DateOfBirth))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.User.Gender))
                .ForMember(dest => dest.BloodType, opt => opt.MapFrom(src => src.User.BloodType))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.User.Address))
                .ForMember(dest => dest.Occupation, opt => opt.MapFrom(src => src.User.Occupation))
                .ForMember(dest => dest.InsuranceId, opt => opt.MapFrom(src => src.User.InsuranceId))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.User.CreatedAt));

        CreateMap<Patient, PatientDto>()
                .ForMember(dest => dest.PatientId, opt => opt.MapFrom(src => src.PatientId))
                .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.User.DisplayName))
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.User.UserName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.User.PhoneNumber))
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.User.DateOfBirth))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.User.Gender))
                .ForMember(dest => dest.BloodType, opt => opt.MapFrom(src => src.User.BloodType))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.User.Address))
                .ForMember(dest => dest.Occupation, opt => opt.MapFrom(src => src.User.Occupation))
                .ForMember(dest => dest.InsuranceId, opt => opt.MapFrom(src => src.User.InsuranceId))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.User.CreatedAt));

        CreateMap<Receptionist, ReceptionistDto>()
                    .ForMember(dest => dest.ReceptionistId, opt => opt.MapFrom(src => src.ReceptionistId))
                    .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.User.DisplayName))
                    .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.User.UserName))
                    .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
                    .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.User.PhoneNumber))
                    .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.User.DateOfBirth))
                    .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.User.Gender))
                    .ForMember(dest => dest.BloodType, opt => opt.MapFrom(src => src.User.BloodType))
                    .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.User.Address))
                    .ForMember(dest => dest.Occupation, opt => opt.MapFrom(src => src.User.Occupation))
                    .ForMember(dest => dest.InsuranceId, opt => opt.MapFrom(src => src.User.InsuranceId));

        CreateMap<Staff, StaffDto>()
                   .ForMember(dest => dest.StaffId, opt => opt.MapFrom(src => src.StaffId))
                   .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.User.DisplayName))
                   .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
                   .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.User.UserName))
                   .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.User.PhoneNumber))
                   .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.User.DateOfBirth))
                   .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.User.Gender))
                   .ForMember(dest => dest.BloodType, opt => opt.MapFrom(src => src.User.BloodType))
                   .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.User.Address))
                   .ForMember(dest => dest.Occupation, opt => opt.MapFrom(src => src.User.Occupation))
                   .ForMember(dest => dest.InsuranceId, opt => opt.MapFrom(src => src.User.InsuranceId))
                   .ReverseMap();


        CreateMap<Staff, StaffDto>()
                   .ForMember(dest => dest.StaffId, opt => opt.MapFrom(src => src.StaffId))
                   .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.User.DisplayName))
                   .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.User.UserName))
                   .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
                   .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.User.PhoneNumber))
                   .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.User.DateOfBirth))
                   .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.User.Gender))
                   .ForMember(dest => dest.BloodType, opt => opt.MapFrom(src => src.User.BloodType))
                   .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.User.Address))
                   .ForMember(dest => dest.Occupation, opt => opt.MapFrom(src => src.User.Occupation))
                   .ForMember(dest => dest.InsuranceId, opt => opt.MapFrom(src => src.User.InsuranceId));

        CreateMap<Doctor, DoctorDto>()
                .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.User.DisplayName))
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.User.UserName))
                .ForMember(dest => dest.FatherName, opt => opt.MapFrom(src => src.User.FatherName))
                .ForMember(dest => dest.MotherName, opt => opt.MapFrom(src => src.User.MotherName))
                .ForMember(dest => dest.Nationality, opt => opt.MapFrom(src => src.User.Nationality))
                .ForMember(dest => dest.Education, opt => opt.MapFrom(src => src.User.Education))
                .ForMember(dest => dest.MaritalStatus, opt => opt.MapFrom(src => src.User.MaritalStatus))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.User.City))
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.User.State))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.User.PhoneNumber))
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.User.DateOfBirth))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.User.Gender))
                .ForMember(dest => dest.BloodType, opt => opt.MapFrom(src => src.User.BloodType))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.User.Address))
                .ForMember(dest => dest.Occupation, opt => opt.MapFrom(src => src.User.Occupation))
                .ForMember(dest => dest.InsuranceId, opt => opt.MapFrom(src => src.User.InsuranceId))
                .ForMember(dest => dest.AppointmentCount, opt => opt.MapFrom(src => src.Appointments.Count));

    }
}
