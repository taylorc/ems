using AutoMapper;
using Ems.Application.Common.Exceptions;
using Ems.Application.Common.Interfaces;
using Ems.Application.Common.Mappings;
using MediatR;

namespace Ems.Application.Organisation.Commands.UpdateOrganisation;

public record UpdateOrganisationCommand : IRequest
{

    public int Id { get; set; } 
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
    

}

public class UpdateOrganisationCommandHandler : IRequestHandler<UpdateOrganisationCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
    
        public UpdateOrganisationCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
    
        public async Task<Unit> Handle(UpdateOrganisationCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Organisations.FindAsync(request.Id);
            
            if(entity==null){
                throw new NotFoundException(nameof(Domain.Entities.Organisation), request.Id);
            }
    
            if(!string.IsNullOrEmpty(request.Address)) entity.Address = request.Address;
            if(!string.IsNullOrEmpty(request.City)) entity.City = request.City;
            if(!string.IsNullOrEmpty(request.Country)) entity.Country = request.Country;
            if(!string.IsNullOrEmpty(request.Description)) entity.Description = request.Description;
            if(!string.IsNullOrEmpty(request.Email)) entity.Email = request.Email;
            if(!string.IsNullOrEmpty(request.Logo)) entity.Logo = request.Logo;
            if(!string.IsNullOrEmpty(request.Name)) entity.Name = request.Name;
            if(!string.IsNullOrEmpty(request.Phone)) entity.Phone = request.Phone;
            if(!string.IsNullOrEmpty(request.Postcode)) entity.Postcode = request.Postcode;
            if(!string.IsNullOrEmpty(request.State)) entity.State = request.State;
            if(!string.IsNullOrEmpty(request.Status)) entity.Status = request.Status;
            if(!string.IsNullOrEmpty(request.Website)) entity.Website = request.Website;

            await _context.SaveChangesAsync(cancellationToken);
    
            return Unit.Value;
        }
    }