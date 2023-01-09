namespace Ems.Domain.Entities;

public class Organisation: BaseAuditableEntity
{
        public string Name { get; set; }= null!;
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
        public string Status { get; set; } = null!;
        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
}