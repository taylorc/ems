using Ems.Application.Common.Interfaces;

namespace Ems.Infrastructure.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
}
