using System.Security.Cryptography.X509Certificates;
using AutoFixture;
using Ems.Application.Employee.Commands.CreateEmployee;
using Ems.Application.Employee.Commands.UpdateEmployee;
using FluentAssertions;
using NUnit.Framework;

namespace Ems.Application.IntegrationTests.TodoItems.Commands;

using static Testing;

public class UpdateTodoItemTests : BaseTestFixture
{
    // [Test]
    // public async Task ShouldRequireValidTodoItemId()
    // {
    //     var command = new UpdateEmployeeCommand { Id = 99, Title = "New Title" };
    //     await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    // }

    [Test]
    public async Task ShouldUpdateTodoItem()
    {
        Fixture fixture = new();
        var employee = fixture.Create<CreateEmployeeCommand>();
        employee.Title = 1;
        employee.Gender = 1;
        employee.EmployeeType = 0;
        employee.State = 1;
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
