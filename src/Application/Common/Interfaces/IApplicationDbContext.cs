using Microsoft.EntityFrameworkCore;

namespace Ems.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Domain.Entities.Employee> Employees { get; }
    DbSet<Domain.Entities.Organisation> Organisations { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
