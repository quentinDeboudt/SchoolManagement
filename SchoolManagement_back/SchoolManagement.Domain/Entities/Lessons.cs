namespace SchoolManagement.Domain.Entities;

public class Lesson
{
    public int Id { get; set; }
    public int SubjectId { get; set; }
    public Subject Subject { get; set; }
    public int TeacherId { get; set; }
    public Person Teacher { get; set; }
    public ICollection<TeacherLesson> TeacherLessons { get; set; }
    public ICollection<LessonGroup> LessonGroups { get; set; }
    public ICollection<LessonSubject> LessonSubjects { get; set; }
}