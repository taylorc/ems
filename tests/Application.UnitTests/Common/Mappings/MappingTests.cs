using System.Runtime.Serialization;
using AutoFixture.NUnit3;
using AutoMapper;
using Ems.Application.Common.Mappings;
using Ems.Application.Employee.Commands.CreateEmployee;
using NUnit.Framework;
using Ems.Domain.Enums;
using FluentAssertions;

namespace Ems.Application.UnitTests.Common.Mappings;

public class MappingTests
{
    private readonly IConfigurationProvider _configuration;
    private readonly IMapper _mapper;

    public MappingTests()
    {
        _configuration = new MapperConfiguration(config => 
            config.AddProfile<MappingProfile>());

        _mapper = _configuration.CreateMapper();
    }

    [Test]
    public void ShouldHaveValidConfiguration()
    {
        _configuration.AssertConfigurationIsValid();
    }

    [Test]
    [TestCase(typeof(CreateEmployeeCommand), typeof(Domain.Entities.Employee))]
    public void ShouldSupportMappingFromSourceToDestination(Type source, Type destination)
    {
        var instance = GetInstanceOf(source);

        _mapper.Map(instance, source, destination);
    }

    [Test, AutoData]
    public void ShouldMapState(CreateEmployeeCommand command)
    {
        command.State = 1;
        command.Title = 0;
        command.EmployeeType = 0;
        command.Gender = 0;

        var employee = _mapper.Map<CreateEmployeeCommand, Domain.Entities.Employee>(command);

        employee.Title.Should().NotBeNull();
        employee.State.Should().NotBeNull();
        
        employee.State.Should().Be(State.SouthAustralia);
    }

    private object GetInstanceOf(Type type)
    {
        if (type.GetConstructor(Type.EmptyTypes) != null)
            return Activator.CreateInstance(type)!;

        // Type without parameterless constructor
        return FormatterServices.GetUninitializedObject(type);
    }
}
