using Ems.Domain.Enums;
using FluentValidation;

namespace Ems.Application.Employee.Commands.CreateEmployee;

public class CreateEmployeeCommandValidator : AbstractValidator<CreateEmployeeCommand>
{
    public CreateEmployeeCommandValidator()
    {
        RuleFor(v => v.Title)
            .Must(v=> Title.TryFromValue(v, out _));

        RuleFor(v => v.LastName)
            .NotNull()
            .Length(150);
        RuleFor(v => v.FirstName)
            .NotNull()
            .Length(150);
        RuleFor(v => v.City)
            .NotNull()
            .Length(150);

        RuleFor(v => v.Email)
            .NotNull()
            .EmailAddress();
        
        RuleFor(v => v.Country)
            .NotNull()
            .Length(150);
        
        RuleFor(v => v.EmployeeType)
            .Must(v => EmployeeType.TryFromValue(v, out _));
        
        RuleFor(v => v.Gender)
            .Must(v => Gender.TryFromValue(v, out _));
        
        RuleFor(v => v.MiddleName)
            .NotNull()
            .Length(150);
        RuleFor(v => v.Phone)
            .NotNull()
            .Length(150);
        RuleFor(v => v.Postcode)
            .NotNull()
            .Length(4);
        RuleFor(v => v.State)
            .Must(v => State.TryFromValue(v, out _));;
        RuleFor(v => v.Street)
            .NotNull()
            .Length(150);
    }
}
