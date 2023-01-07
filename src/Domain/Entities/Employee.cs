namespace Ems.Domain.Entities;

public class Employee: BaseAuditableEntity
{
    public string Street { get; set; } = null!;
    public string City { get; set; } = null!;
    public State State { get; set; } = null!;
    
    public string Country { get; set;} = null!;
    public string Postcode { get; set; } = null!;
    public string? FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string? LastName { get; set; }
    public Title Title { get; set; } = null!;
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public Gender Gender { get; set;} = null!;
    public EmployeeType EmployeeType { get; set; } = null!;
    public bool IsAdmin { get; set; }
    public Employee? Manager { get; set; }
    public ICollection<Employee> Reports { get; set; } = null!;
}
