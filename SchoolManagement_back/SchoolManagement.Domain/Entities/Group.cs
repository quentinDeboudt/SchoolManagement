namespace SchoolManagement.Domain.Entities;

public class Group
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int ClassroomId { get; set; }
    public Classroom Classroom { get; set; }
    public ICollection<Person> Students { get; set; } = new List<Person>();
    public ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
}