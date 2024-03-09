using FluentValidation;

namespace Application.Accountants
{
    /// <summary>
    /// Validates properties of the AccountantDto class.
    /// </summary>
    public class AccountantValidator : AbstractValidator<AccoutantDto>
    {
        public AccountantValidator()
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
