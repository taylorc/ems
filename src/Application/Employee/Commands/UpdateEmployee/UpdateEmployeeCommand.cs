using AutoMapper;
using Ems.Application.Common.Exceptions;
using Ems.Application.Common.Interfaces;
using Ems.Application.Common.Mappings;
using Ems.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ems.Application.Employee.Commands.UpdateEmployee;

public record UpdateEmployeeCommand : IRequest
{
    public int Id { get; set; }

    public string Street { get; set; } = null!;
    public string City { get; set; } = null!;
    public int? State { get; set; }
    public string Country { get; set; } = null!;
    public string Postcode { get; set; } = null!;

    public string? FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string? LastName { get; set; }
    public Title? Title { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public string Email { get; set; } = null!;
    public Gender? Gender { get; set; } = null!;
    public EmployeeType? EmployeeType { get; set; } = null!;
    public bool? IsAdmin { get; set; }

    public int? ManagerId { get; set; }
    
}

public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public UpdateEmployeeCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Employees
            .FindAsync(new object[] { request.Id }, cancellationToken);
        
        if (entity == null)
        {
            throw new NotFoundException(nameof(Employee), request.Id);
        }

        if (request.ManagerId.HasValue)
        {
            entity.Manager = await _context.Employees.FindAsync(new { request.ManagerId });
        }
        
        if(!string.IsNullOrEmpty(request.LastName))  entity.LastName = request.LastName;
        if(!string.IsNullOrEmpty(request.FirstName)) entity.FirstName = request.FirstName;
        if(!string.IsNullOrEmpty(request.Phone)) entity.Phone = request.Phone;
        if(!string.IsNullOrEmpty(request.Email)) entity.Email = request.Email;
        if(request.Gender!=null)entity.Gender = request.Gender;
        if(request.EmployeeType!=null)entity.EmployeeType = request.EmployeeType;
        if(!string.IsNullOrEmpty(request.Street) && request.State.HasValue)  {
            entity.Street = request.Street;
            entity.City = request.City;
            entity.Postcode = request.Postcode;
            request.State = State.FromValue(request.State.Value);
            request.Country = request.Country;
        }
        if(request.IsAdmin.HasValue) entity.IsAdmin = request.IsAdmin.Value;
        
        if(!string.IsNullOrEmpty(request.MiddleName)) entity.MiddleName = request.MiddleName;
        
        if(request.Title!=null)entity.Title = request.Title;

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
