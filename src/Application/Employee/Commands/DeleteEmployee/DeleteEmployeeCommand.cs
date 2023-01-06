﻿using Ems.Application.Common.Exceptions;
using Ems.Application.Common.Interfaces;
using MediatR;

namespace Ems.Application.Employee.Commands.DeleteEmployee;

public record DeleteEmployeeCommand(int Id) : IRequest;

public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteEmployeeCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Employees
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Employee), request.Id);
        }

        _context.Employees.Remove(entity);

        //entity.AddDomainEvent(new EmployeeDeletedEvent(entity));

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
