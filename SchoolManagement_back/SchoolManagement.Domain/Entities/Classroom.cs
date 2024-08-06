namespace SchoolManagement.Domain.Entities;

public class Classroom
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<Group> Groups { get; set; } = new List<Group>();
    public ICollection<Person> Teachers { get; set; } = new List<Person>();
}