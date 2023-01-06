using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ems.Domain.ValueObjects;
public class Address:ValueObject
{

    public String Street { get; } = null!;
    public String City { get; } = null!;
    public State State { get; } = null!;
    public String Country { get; } = null!;
    public String Postcode { get; } = null!;

    public Address() { }

    public Address(string street, string city, State state, string country, string postcode)
    {
        Street = street;
        City = city;
        State = state;
        Country = country;
        Postcode = postcode;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        // Using a yield return statement to return each element one at a time
        yield return Street;
        yield return City;
        yield return State;
        yield return Country;
        yield return Postcode;
    }
}
