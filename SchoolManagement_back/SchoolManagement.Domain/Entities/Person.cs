namespace SchoolManagement.Domain.Entities;

public class Person
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public ICollection<PersonRole> PersonRoles { get; set; }
    public ICollection<StudentGroup> StudentGroups { get; set; }
    public ICollection<TeacherClassroom> TeacherClassrooms { get; set; }
    public ICollection<TeacherLesson> TeacherLessons { get; set; }
}