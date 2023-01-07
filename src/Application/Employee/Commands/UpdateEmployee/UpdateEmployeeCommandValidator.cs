using Ardalis.SmartEnum;
using Ems.Application.Common.Helpers;
using Ems.Domain.Enums;
using FluentValidation;
using FluentValidation.Validators;

namespace Ems.Application.Employee.Commands.UpdateEmployee;

public class UpdateEmployeeCommandValidator : AbstractValidator<UpdateEmployeeCommand>
{
    public UpdateEmployeeCommandValidator()
    {
        RuleFor(v => v.Title)
            .Must(v=> Title.TryFromValue(v.Value, out _))
            .When(x=>x.Title!=null, ApplyConditionTo.CurrentValidator);

        // RuleFor(v => v.LastName)
        //     .NotNull();
        // RuleFor(v => v.FirstName)
        //     .NotNull();
        // RuleFor(v => v.City)
        //     .NotNull();
        //
        RuleFor(v => v.Email)
            .EmailAddress()
            .When(x=>!string.IsNullOrEmpty(x.Email));
        // RuleFor(v => v.Country)
        //     .NotNull();
        RuleFor(v => v.EmployeeType)
            .Must(v => EmployeeType.TryFromValue(v.Value, out _))
            .When(x=>x.EmployeeType!=null, ApplyConditionTo.CurrentValidator);
        RuleFor(v => v.Gender)
            .Must(v => Gender.TryFromValue(v.Value, out _))
            .When(x=>x.Gender!=null, ApplyConditionTo.CurrentValidator);
        // RuleFor(v => v.IsAdmin)
        //     .NotNull();
        // RuleFor(v => v.MiddleName)
        //     .NotNull();
        // RuleFor(v => v.Phone)
        //     .NotNull();
        // RuleFor(v => v.Postcode)
        //     .NotNull();
        // RuleFor(v => v.State)
        //     .NotNull();
        // RuleFor(v => v.Street)
        //     .NotNull();
    }
}
