using Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MedicalHistoreis
{
    public class MedicalHistoryValidators : AbstractValidator<MedicalHistory>
    {
        public MedicalHistoryValidators() 
        {
            RuleFor(x => x.Height).NotEmpty();
            RuleFor(x => x.Weight).NotEmpty();
        }
    }
}
