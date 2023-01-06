using Microsoft.EntityFrameworkCore;

namespace Ems.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Domain.Entities.Employee> Employees { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
