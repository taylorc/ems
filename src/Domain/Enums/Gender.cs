using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.SmartEnum;

namespace Ems.Domain.Enums;

public class Gender:SmartEnum<Gender>
{
    public static readonly Gender None = new(nameof(None), 0);
    public static readonly Gender Female = new(nameof(Female), 1);
    public static readonly Gender Male = new(nameof(Male), 2);
    public static readonly Gender NonBinary = new("Non Binary", 3);

    public Gender(string name, int value) : base(name, value)
    {
    }
}
