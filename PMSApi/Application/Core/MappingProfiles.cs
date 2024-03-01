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
    }
}
