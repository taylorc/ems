using AutoFixture;
using AutoFixture.NUnit3;

namespace Ems.Application.IntegrationTests.AutoFixture;

public class EmsAutoData : AutoDataAttribute
{
    public EmsAutoData() : base(() =>
    {
        var fixture = new Fixture();
        fixture.Customizations.Add(new SmartEnumSpecimanBuilder());

        return fixture;
    })
    {

    }
}