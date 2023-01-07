using System.Reflection;
using Ardalis.SmartEnum;
using AutoFixture.Kernel;
using Ems.Domain.Enums;

namespace Ems.Application.IntegrationTests.AutoFixture;

public class SmartEnumSpecimanBuilder: ISpecimenBuilder
{
    public object Create(object request, ISpecimenContext context)
    {
        var rnd = new Random();
        if (request is not PropertyInfo pi || pi.PropertyType != typeof(int))
        {
            return new NoSpecimen();
        }

        return pi.Name switch
        {
            "Gender" => rnd.Next(0, Gender.List.Count - 1),
            "State" => rnd.Next(0, State.List.Count - 1),
            "EmployeeType" => rnd.Next(0, EmployeeType.List.Count - 1),
            "Title" => rnd.Next(0, Title.List.Count - 1),
            _ => new NoSpecimen()
        };
    }
}
