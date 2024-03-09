using FluentValidation;

namespace Application.Nurses
{
    /// <summary>
    /// Provides validation rules for the properties of a NurseDto.
    /// </summary>
    public class NurseValidators : AbstractValidator<NurseDto>
    {
        public NurseValidators()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.PhoneNumber).NotEmpty();
            RuleFor(x => x.Address).NotEmpty();
            RuleFor(x => x.City).NotEmpty();
            RuleFor(x => x.ZipCode).NotEmpty();
            RuleFor(x => x.Occupation).NotEmpty();
            RuleFor(x => x.State).NotEmpty();
        }
    }
}
