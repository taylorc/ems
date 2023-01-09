using Ems.Application.Employee.Commands.CreateEmployee;
using Ems.Application.Employee.Commands.UpdateEmployee;
using Ems.Application.IntegrationTests.AutoFixture;
using FluentAssertions;
using NUnit.Framework;

namespace Ems.Application.IntegrationTests.Employee.Commands;

using static Testing;

public class UpdateEmployeeCommandTests : BaseTestFixture
{
    // [Test]
    // public async Task ShouldRequireValidTodoItemId()
    // {
    //     var command = new UpdateEmployeeCommand { Id = 99, Title = "New Title" };
    //     await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    // }

    [Test, EmsAutoData]
    public async Task ShouldUpdateTodoItem(CreateEmployeeCommand employee)
    {
        employee.Postcode = "3977";
        employee.ReportIds = new List<int>();
        employee.Email = "test1@test.com";

        var employeeId = await SendAsync(employee);

        string testEmail = "test@test.com";
        var command = new UpdateEmployeeCommand()
        {
            Id = employeeId,
            Email = testEmail
        };

        await SendAsync(command);

        var item = await FindAsync<Domain.Entities.Employee>(employeeId);

        item.Should().NotBeNull();
        item!.Email.Should().Be(testEmail);
        item.LastModifiedBy.Should().NotBeNull();
        item.LastModifiedBy.Should().Be("SYSTEM");
        item.LastModified.Should().NotBeNull();
        item.LastModified.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
    }
}
