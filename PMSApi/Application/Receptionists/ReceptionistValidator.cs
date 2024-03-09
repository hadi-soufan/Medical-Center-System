using Application.Receptionist;
using FluentValidation;

namespace Application.Receptionists
{
    /// <summary>
    /// Validator for the ReceptionistDto class.
    /// </summary>
    public class ReceptionistValidator : AbstractValidator<ReceptionistDto>
    {
        public ReceptionistValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required.");
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Phone number is required.");
            RuleFor(x => x.Address).NotEmpty().WithMessage("Address is required.");
            RuleFor(x => x.City).NotEmpty().WithMessage("City is required.");
            RuleFor(x => x.ZipCode).NotEmpty().WithMessage("Zip code is required.");
            RuleFor(x => x.Occupation).NotEmpty().WithMessage("Occupation is required.");
            RuleFor(x => x.State).NotEmpty().WithMessage("State is required.");
        }
    }
}
