using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.SmartEnum;

namespace Ems.Domain.Enums;

public class State: SmartEnum<State>
{
    public static readonly State None = new(nameof(None), 0);
    public static readonly State SouthAustralia = new("South Australia", 1);
    public static readonly State Victoria = new(nameof(Victoria), 2);
    public static readonly State NorthernTerritory = new("Northern Territory", 3);
    public static readonly State WesternAustralia = new("Western Australia", 4);
    public static readonly State Queensland = new("Queensland", 5);
    public static readonly State NewSouthWales = new("New South Wales", 6);
    public static readonly State Tasmania = new(nameof(Tasmania), 7);
    public static readonly State AustralianCapitalTerritory = new("Australian Capital Territory", 8);

    public State(string name, int value) : base(name, value)
    {
    }
}
