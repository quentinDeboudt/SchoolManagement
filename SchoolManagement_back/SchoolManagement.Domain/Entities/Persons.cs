namespace Domain.Entities;

public class Persons
{
    public Guid Id {get; set; }
    public required string Firstname {get; set; }
    public required string Lastname {get; set; }
}