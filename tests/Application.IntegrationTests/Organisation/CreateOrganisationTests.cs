using Ems.Application.Employee.Commands.CreateEmployee;
using Ems.Application.IntegrationTests.AutoFixture;
using Ems.Application.Organisation.Commands.CreateOrganisation;
using FluentAssertions;
using NUnit.Framework;

namespace Ems.Application.IntegrationTests.Organisation;

using static Testing;

public class CreateOrganisationTests: BaseTestFixture
{
    // [Test]
    // public async Task ShouldRequireMinimumFields()
    // {
    //     var command = new CreateEmployeeCommand();
    //
    //     await FluentActions.Invoking(() =>
    //         SendAsync(command)).Should().ThrowAsync<ValidationException>();
    // }

    [Test, EmsAutoData]
    public async Task ShouldCreateOrganisation(CreateOrganisationCommand command)
    {
        command.Postcode = "3977";
        command.Email = "test@test.com";

        var itemId = await SendAsync(command);

        var item = await FindAsync<Domain.Entities.Organisation>(itemId);

        item.Should().NotBeNull();
        item!.Email.Should().Be(command.Email);
        item.LastModifiedBy.Should().NotBeNull();
        item.LastModifiedBy.Should().Be("SYSTEM");
        item.LastModified.Should().NotBeNull();
        item.LastModified.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
        item.Description.Should().Be(command.Description);
        item.Name.Should().Be(command.Name);
        item.Phone.Should().Be(command.Phone);
        item.Postcode.Should().Be(command.Postcode);
        
    }
}