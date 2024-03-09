using FluentValidation;

namespace Application.Doctors
{
    /// <summary>
    /// Provides validation rules for DoctorDto objects.
    /// </summary>
    public class DoctorValidators : AbstractValidator<DoctorDto>
    {
        public DoctorValidators()
        {
            RuleFor(x => x.Email).NotEmpty();
            RuleFor(x => x.PhoneNumber).NotEmpty();
            RuleFor(x => x.Address).NotEmpty();
            RuleFor(x => x.City).NotEmpty();
            RuleFor(x => x.ZipCode).NotEmpty();
            RuleFor(x => x.Occupation).NotEmpty();
            RuleFor(x => x.State).NotEmpty();
        }
    }
}
