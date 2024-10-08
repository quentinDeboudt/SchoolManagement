namespace SchoolManagement.Domain.Entities;

public class Lesson
{
    public int Id { get; set; }
    public int? SubjectId { get; set; }
    public Subject? Subject { get; set; }
    public ICollection<Person> Teachers { get; set; } = new List<Person>();
    public ICollection<Group> Groups { get; set; } = new List<Group>(); 
}