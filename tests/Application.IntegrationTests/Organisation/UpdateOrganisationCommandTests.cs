using Ems.Application.Organisation.Commands.CreateOrganisation;
using Ems.Application.Organisation.Commands.UpdateOrganisation;
using Ems.Application.IntegrationTests.AutoFixture;
using FluentAssertions;
using NUnit.Framework;

namespace Ems.Application.IntegrationTests.Organisation;

using static Testing;

public class UpdateOrganisationCommandTests : BaseTestFixture
{
    // [Test]
    // public async Task ShouldRequireValidTodoItemId()
    // {
    //     var command = new UpdateEOrganisationCommand { Id = 99, Title = "New Title" };
    //     await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    // }

    [Test, EmsAutoData]
    public async Task ShouldUpdateOrganisation(CreateOrganisationCommand organisation)
    {
        
        organisation.Postcode = "3977";
        organisation.Email = "test1@test.com";

        var organisationId = await SendAsync(organisation);

        string testEmail = "test@test.com";
        var command = new UpdateOrganisationCommand()
        {
            Id = organisationId,
            Email = testEmail
        };

        await SendAsync(command);

        var item = await FindAsync<Domain.Entities.Organisation>(organisationId);

        item.Should().NotBeNull();
        item!.Email.Should().Be(testEmail);
        item.LastModifiedBy.Should().NotBeNull();
        item.LastModifiedBy.Should().Be("SYSTEM");
        item.LastModified.Should().NotBeNull();
        item.LastModified.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
        item.Description.Should().Be(organisation.Description);
    }
}
