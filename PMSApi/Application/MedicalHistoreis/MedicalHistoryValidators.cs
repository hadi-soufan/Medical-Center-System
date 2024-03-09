using Domain.Entities;
using FluentValidation;

namespace Application.MedicalHistoreis
{
    /// <summary>
    /// Represents a validator for the MedicalHistory entity.
    /// </summary>
    public class MedicalHistoryValidators : AbstractValidator<MedicalHistory>
    {
        public MedicalHistoryValidators()
        {
            RuleFor(x => x.Height).NotEmpty();
            RuleFor(x => x.Weight).NotEmpty();
        }
    }
}
