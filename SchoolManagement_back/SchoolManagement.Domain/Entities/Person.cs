namespace SchoolManagement.Domain.Entities;

public class Person
{
     public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public ICollection<Role> Roles { get; set; } = new List<Role>();
    public ICollection<Group> StudentGroups { get; set; } = new List<Group>();
    public ICollection<Classroom> TeacherClassrooms { get; set; } = new List<Classroom>();
    public ICollection<Lesson> TeacherLessons { get; set; } = new List<Lesson>();
}