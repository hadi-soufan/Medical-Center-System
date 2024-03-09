using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Staffs
{
    /// <summary>
    /// Validator for the StaffDto class.
    /// </summary>
    public class StaffValidator : AbstractValidator<StaffDto> 
    {
        public StaffValidator() 
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
