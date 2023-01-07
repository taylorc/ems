using AutoFixture.NUnit3;
using Ems.Application.Employee.Commands.CreateEmployee;
using Ems.Domain.Enums;
using FluentAssertions;
using NUnit.Framework;

namespace Ems.Application.IntegrationTests.TodoItems.Commands;

using static Testing;

public class CreateEmployeeTests : BaseTestFixture
{
    // [Test]
    // public async Task ShouldRequireMinimumFields()
    // {
    //     var command = new CreateTodoItemCommand();
    //
    //     await FluentActions.Invoking(() =>
    //         SendAsync(command)).Should().ThrowAsync<ValidationException>();
    // }

    [Test, AutoData]
    public async Task ShouldCreateEmployee(CreateEmployeeCommand command)
    {
        command.Title = 1;
        command.Gender = 1;
        command.EmployeeType = 0;
        command.State = 1;
        command.ReportIds = new List<int>();

        var itemId = await SendAsync(command);

        var item = await FindAsync<Domain.Entities.Employee>(itemId);

        item.Should().NotBeNull();
        item!.Email.Should().Be(command.Email);
        item.LastModifiedBy.Should().NotBeNull();
        item.LastModifiedBy.Should().Be("SYSTEM");
        item.LastModified.Should().NotBeNull();
        item.LastModified.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
        item.Gender.Should().Be(Gender.Female);
    }
}
