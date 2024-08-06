namespace SchoolManagement.Domain.Entities;

public class Subject
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<LessonSubject> LessonSubjects { get; set; }
}