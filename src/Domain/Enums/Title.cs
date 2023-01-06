using Ardalis.SmartEnum;

namespace Ems.Domain.Enums;

public class Title: SmartEnum<Title>
{
    public static readonly Title None = new(nameof(None), 0);
    public static readonly Title Mr = new(nameof(Mr), 1);
    public static readonly Title Miss = new(nameof(Miss), 2);
    public static readonly Title Mrs = new(nameof(Mrs), 3);
    public static readonly Title Master = new(nameof(Master), 4);
    public static readonly Title Professor = new(nameof(Professor), 5);
    public static readonly Title Doctor = new(nameof(Doctor), 6);
    public static readonly Title Reverend = new(nameof(Reverend), 7);

    public Title(string name, int value) : base(name, value)
    {
    }
}
