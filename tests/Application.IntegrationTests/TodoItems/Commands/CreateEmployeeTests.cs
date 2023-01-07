using AutoFixture.NUnit3;
using Ems.Application.Common.Exceptions;
using Ems.Application.Employee.Commands.CreateEmployee;
using Ems.Application.IntegrationTests.AutoFixture;
using Ems.Domain.Enums;
using FluentAssertions;
using NUnit.Framework;

namespace Ems.Application.IntegrationTests.TodoItems.Commands;

using static Testing;

public class CreateEmployeeTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireMinimumFields()
    {
        var command = new CreateEmployeeCommand();
    
        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    [Test, EmsAutoData]
    public async Task ShouldCreateEmployee(CreateEmployeeCommand command)
    {
        command.Postcode = "3977";
        command.Email = "test@test.com";
        command.ReportIds = new List<int>();

        var itemId = await SendAsync(command);

        var item = await FindAsync<Domain.Entities.Employee>(itemId);

        item.Should().NotBeNull();
        item!.Email.Should().Be(command.Email);
        item.LastModifiedBy.Should().NotBeNull();
        item.LastModifiedBy.Should().Be("SYSTEM");
        item.LastModified.Should().NotBeNull();
        item.LastModified.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
        item.Gender.Value.Should().Be(command.Gender);
    }
}
