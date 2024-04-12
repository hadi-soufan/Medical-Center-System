using Domain.Entities;
using FluentValidation;

namespace Application.Appoitments
{
    /// <summary>
    /// Validates properties of the Appointment entity.
    /// </summary>
    public class AppointmentValidator : AbstractValidator<Appointment>
    {
        public AppointmentValidator() 
        { 
            RuleFor(x => x.AppointmentStatus).NotEmpty();
            RuleFor(x => x.AppointmentDateStart).NotEmpty();
            RuleFor(x => x.AppointmentDateEnd).NotEmpty();
            RuleFor(x => x.AppointmentStatus).NotEmpty();
            RuleFor(x => x.AppointmentType).NotEmpty();
        }
    }
}
