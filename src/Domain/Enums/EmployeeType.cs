using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.SmartEnum;

namespace Ems.Domain.Enums;

public class EmployeeType:SmartEnum<EmployeeType>
{
    public static readonly EmployeeType Normal = new(nameof(Normal), 0);
    public static readonly EmployeeType TeamLead = new("Team Lead", 1);
    public static readonly EmployeeType Manager = new(nameof(Manager), 2);
    public static readonly EmployeeType Executive = new(nameof(Executive), 3);

    public EmployeeType(string name, int value) : base(name, value)
    {
    }
}
