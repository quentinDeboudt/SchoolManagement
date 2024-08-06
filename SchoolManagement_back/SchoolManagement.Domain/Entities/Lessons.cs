namespace Domain.Entities;

public class Lessons
{
    public Guid Id {get; set; }
    public required string Subject_Id {get; set; }
    public required string Teachers_Id {get; set; }
}