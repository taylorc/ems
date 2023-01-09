using AutoMapper;
using Ems.Application.Common.Interfaces;
using Ems.Application.Common.Mappings;
using Ems.Application.Organisation.Commands.UpdateOrganisation;
using MediatR;

namespace Ems.Application.Organisation.Commands.CreateOrganisation;

// ReSharper disable once ClassNeverInstantiated.Global
public class CreateOrganisationCommand : IRequest<int>, IMapTo<Domain.Entities.Organisation>
{
    public string Name { get; set; } = null!;
    public string Address { get; set; }= null!;
    public string City { get; set; }= null!;
    public string State { get; set; }= null!;
    public string Country { get; set; }= null!;
    public string Postcode { get; set; }= null!;
    public string Phone { get; set; }= null!;
    public string Email { get; set; }= null!;
    public string Website { get; set; }= null!;
    public string Logo { get; set; }= null!;
    public string Description { get; set; }= null!;
    public string Status { get; set; }= null!;
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateOrganisationCommand, Domain.Entities.Organisation>()
            .ForMember(d => d.Id, opt => opt.Ignore())
            .ForMember(d=>d.LastModifiedBy,opt=>opt.Ignore())
            .ForMember(d=>d.Created,opt=>opt.Ignore())
            .ForMember(d=>d.LastModified,opt=>opt.Ignore())
            .ForMember(d=>d.DomainEvents,opt=>opt.Ignore())
            .ForMember(d => d.CreatedBy, opt => opt.Ignore())
            .ForMember(d=>d.Employees, opt=>opt.Ignore());
    }
    
}

public class CreateOrganisationCommandHandler : IRequestHandler<CreateOrganisationCommand, int>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public CreateOrganisationCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreateOrganisationCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<CreateOrganisationCommand, Domain.Entities.Organisation>(request);

        _context.Organisations.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}