﻿using Domain.Entities;
using FluentValidation;

namespace Application.Appoitments
{
    public class AppointmentValidator : AbstractValidator<Appointment>
    {
        public AppointmentValidator() 
        { 
            RuleFor(x => x.AppointmentStatus).NotEmpty();
            RuleFor(x => x.AppointmentDate).NotEmpty();
            RuleFor(x => x.AppointmentStatus).NotEmpty();
            RuleFor(x => x.AppointmentType).NotEmpty();
        }
    }
}
