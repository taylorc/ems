using AutoMapper;
using Ems.Application.Common.Interfaces;
using Ems.Application.Common.Mappings;

using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ems.Application.Employee.Commands.CreateEmployee;

public record CreateEmployeeCommand : IRequest<int>, IMapTo<Domain.Entities.Employee>
{
    public string Street { get; set; } = null!;
    public string City { get; set; }= null!;
    public int State { get; set; }
    public string Country { get; set; }= null!;
    public string Postcode { get; set; }= null!;

    public string? FirstName { get; set; }= null!;
    public string? MiddleName { get; set; }= null!;
    public string? LastName { get; set; }= null!;
    public int Title { get; set; }
    public string? FaxNumber { get; set; }
    public string? Phone { get; set; }= null!;
    public string? Email { get; set; }= null!;
    public int Gender { get; set; }
    public int EmployeeType { get; set; }
    public bool IsAdmin { get; set; }

    public int ManagerId { get; set; }

    public List<int>? ReportIds { get; set; }

    public void Mapping(Profile profile)
    {

        profile.CreateMap<CreateEmployeeCommand, Domain.Entities.Employee>()
            .ForMember(x=>x.Manager, s=>s.Ignore())
            .ForMember(d => d.Created, s => s.Ignore())
            .ForMember(d => d.CreatedBy, s => s.Ignore())
            .ForMember(d => d.LastModified, s => s.Ignore())
            .ForMember(d => d.LastModifiedBy, s => s.Ignore())
            .ForMember(d => d.Id, s => s.Ignore())
            .ForMember(d => d.DomainEvents, s => s.Ignore())
            .ForMember(d => d.Reports, s => s.Ignore());
    }
}


public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, int>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public CreateEmployeeCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {

        var entity = _mapper.Map<CreateEmployeeCommand, Domain.Entities.Employee>(request);

        entity.Manager = request.ManagerId == 0 ? null : 
            await _context.Employees.FirstOrDefaultAsync(x=>x.Id==request.ManagerId, cancellationToken: cancellationToken);

        if (request.ReportIds != null && request.ReportIds.Any())
        {
            entity.Reports = await BuildReportsList(request.ReportIds);
        }

        //entity.AddDomainEvent(new EmployeeCreatedEvent(entity));

        _context.Employees.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }

    private async Task<ICollection<Domain.Entities.Employee>> BuildReportsList(List<int> requestReportIds)
    {
        IList<Domain.Entities.Employee> employees = new List<Domain.Entities.Employee>();

        foreach (var id in requestReportIds)
        {
            employees.Add(await _context.Employees.SingleAsync(x=>x.Id==id));
        }

        return employees;
    }
}
